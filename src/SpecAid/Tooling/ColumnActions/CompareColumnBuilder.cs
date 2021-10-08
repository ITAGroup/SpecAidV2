using System.Collections.Generic;
using SpecAid.Tooling.Base;

namespace SpecAid.Tooling.ColumnActions
{
    public class CompareColumnBuilder<T>
    {
        private readonly ColumnActionFactory _columnActionFactory;

        private readonly List<ColumnToActionContainer<IComparerColumnAction>> _columnOverrides = new List<ColumnToActionContainer<IComparerColumnAction>>();

        public CompareColumnBuilder(ColumnActionFactory columnActionFactory)
        {
            _columnActionFactory = columnActionFactory;
        }

        public CompareColumnBuilder<T> AddIgnore(string columnName)
        {
            var aOverride = new ColumnToActionContainer<IComparerColumnAction>();
            aOverride.ColumnName = columnName;
            aOverride.MatchAction = new IgnoreAction(columnName);
            _columnOverrides.Add(aOverride);
            return this;
        }

        public CompareColumnBuilder<T> AddSymbolic(string columnName, string symbolic)
        {
            var action = _columnActionFactory.GetAction<IComparerColumnAction>(typeof(T), symbolic);

            var aOverride = new ColumnToActionContainer<IComparerColumnAction>();
            aOverride.ColumnName = columnName;
            aOverride.MatchAction = action;
            _columnOverrides.Add(aOverride);

            return this;
        }

        public List<ColumnToActionContainer<IComparerColumnAction>> Out => _columnOverrides;
    }
}
