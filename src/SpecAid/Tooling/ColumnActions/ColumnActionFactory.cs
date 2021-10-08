using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;
using TechTalk.SpecFlow;

namespace SpecAid.Tooling.ColumnActions
{
    public class ColumnActionFactory
    {
        private readonly RecallAid _recallAid;
        private readonly Translator _translator;
        private readonly SetTranslator _setTranslator;

        public ColumnActionFactory(
            RecallAid recallAid,
            Translator translator,
            SetTranslator setTranslator)
        {
            _recallAid = recallAid;
            _translator = translator;
            _setTranslator = setTranslator;
        }

        /// <summary>
        /// Gets the appropriate action for the given interface and column name
        /// </summary>
        /// <typeparam name="TColumnAction"></typeparam>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public TColumnAction GetAction<TColumnAction>(Type type, string columnName) where TColumnAction : IColumnAction
        {
            var actions = GetActions<TColumnAction>(type, columnName);

            foreach (var action in actions)
            {
                if (action.UseWhen())
                {
                    return action;
                }
            }

            // With a Unknown Action... We will NEVER get here.  Unless the Unknown Action is turned off.

            throw new ArgumentException("Unable to determine what to do with column:" + columnName);
        }
        
        /// <summary>
        /// Do all of the types that are of match action and given interface
        /// </summary>
        /// <typeparam name="TColumnAction"></typeparam>
        /// <param name="fieldType"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks>May want to change this to be lazy loaded so i'm not creating this all of the time</remarks>

        private IEnumerable<TColumnAction> GetActions<TColumnAction>
            (Type fieldType, string columnName) where TColumnAction : IColumnAction
        {
            var actions = GetColumnActionList<TColumnAction>()
                .Select(action => (TColumnAction)Activator.CreateInstance(
                    action,
                    _recallAid, _translator, _setTranslator, this,
                    fieldType, columnName))
                .OrderBy(t => t.ConsiderOrder);

            return actions;
        }

        readonly Dictionary<Type,object> _cachedTypes = new Dictionary<Type, object>();

        private IList<Type> GetColumnActionList<TColumnAction>()
        {
            if (_cachedTypes.ContainsKey(typeof(TColumnAction)))
                return (IList<Type>) _cachedTypes[typeof(TColumnAction)];

            var specAidAssembly = Assembly.GetExecutingAssembly();

            var specAidTypes = specAidAssembly.GetTypes()
                .Where(typeof(TColumnAction).IsAssignableFrom)
                .Where(typeof(ColumnAction).IsAssignableFrom);

            var testAssembly = AssemblyEntryFinderInUnitTests.Go();

            var testTypes = testAssembly.GetTypes()
                .Where(typeof(TColumnAction).IsAssignableFrom)
                .Where(typeof(ColumnAction).IsAssignableFrom);

            var types = specAidTypes
                .Union(testTypes)
                .ToList();

            _cachedTypes[typeof(TColumnAction)] = types;
            return types;
        }

        /// <summary>
        /// Method to match columns in a table to properties in a object
        /// </summary>
        public IEnumerable<ColumnToActionContainer<T>> GetActionsFromColumns<T>(Table table, Type target)
            where T : IColumnAction
        {
            return GetActionsFromColumns<T>(table, target, null);
        }

        /// <summary>
        /// Method to match columns in a table to properties in a object
        /// </summary>
        public IEnumerable<ColumnToActionContainer<T>> GetActionsFromColumns<T>(
            Table table, Type target, IEnumerable<ColumnToActionContainer<T>> overrides )
            where T : IColumnAction
        {
            //setup my return object
            var columnToActions = new List<ColumnToActionContainer<T>>();

            if (overrides != null)
            {
                foreach (var aOverride in overrides)
                {
                    if (aOverride.ColumnIndex == null)
                    {
                        // convert name to index
                        for (var i = 0; i < table.Header.Count; i++)
                        {
                            if (table.Header.ElementAt(i) == aOverride.ColumnName)
                            {
                                aOverride.ColumnIndex = i;
                                columnToActions.Add(aOverride);
                            }
                        }
                        continue;
                    }

                    if (aOverride.ColumnName == null)
                    {
                        aOverride.ColumnName = table.Header.ElementAt(aOverride.ColumnIndex.Value);
                        columnToActions.Add(aOverride);
                        continue;
                    }

                    columnToActions.Add(aOverride);
                }
            }

            //run through each column and set the matching prop
            for (var tableColumnIndex = 0; tableColumnIndex < table.Header.Count(); tableColumnIndex++)
            {
                if (columnToActions.Any(x => x.ColumnIndex == tableColumnIndex))
                    continue;

                var headerItem = table.Header.ElementAt(tableColumnIndex);

                var columnAction = new ColumnToActionContainer<T>()
                {
                    ColumnIndex = tableColumnIndex,
                    ColumnName = headerItem,
                    MatchAction = GetAction<T>(target, headerItem)
                };
                columnToActions.Add(columnAction);
            }

            //Print needs these in the same order as the columns
            var sortedActions = columnToActions.OrderBy(x => x.ColumnIndex);

            //return the mappings of the successful columns
            return sortedActions;
        }
    }
}
