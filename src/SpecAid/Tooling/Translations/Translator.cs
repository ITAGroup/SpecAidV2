using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Helper;

namespace SpecAid.Tooling.Translations
{
    public class Translator
    {
        private readonly RecallAid _recallAid;

        public Translator(RecallAid recallAid)
        {
            _recallAid = recallAid;
        }

        /// <summary>
        /// Gets the appropriate value for the cell
        /// </summary>
        public object Translate(PropertyInfo info, string tableValue)
        {
            return TranslateContinueAfterOperation(info, tableValue, -1);
        }

        public object TranslateContinueAfterOperation(
            PropertyInfo info, string tableValue, int operationToContinueAfter)
        {
            var translations = GetTranslations();

            foreach (var action in translations)
            {
                if (action.ConsiderOrder <= operationToContinueAfter)
                    continue;

                if (!(action.UseWhen(info, tableValue)))
                    continue;

                return action.Do(info, tableValue);
            }

            return tableValue;
        }

        private IEnumerable<ITranslation> _cellActions;

        /// <summary>
        /// Do all of the cell actions
        /// TODO: Factory Pattern
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ITranslation> GetTranslations()
        {
            if (_cellActions != null)
                return _cellActions;

            var specAidAssembly = Assembly.GetExecutingAssembly();

            var specAidTypes = specAidAssembly.GetTypes()
                .Where(typeof(Translation).IsAssignableFrom)
                .Where(x => !x.IsAbstract)
                .ToList();

            var testAssembly = AssemblyEntryFinderInUnitTests.Go();

            var testTypes = testAssembly.GetTypes()
                .Where(typeof(Translation).IsAssignableFrom)
                .Where(x => !x.IsAbstract)
                .ToList();

            var translations = specAidTypes
                .Union(testTypes)
                .Select(action =>
                    (ITranslation)Activator.CreateInstance(action, this, _recallAid))
                .OrderBy(t => t.ConsiderOrder)
                .ToList();

            _cellActions = translations;
            return _cellActions;
        }
    }
}
