using System.Collections.Generic;
using SpecAid.Tooling.Base;

namespace SpecAid.Tooling.ColumnActions
{
    public class CreateColumnBuilder<T>
    {
        private readonly ColumnActionFactory _columnActionFactory;

        private readonly List<ColumnToActionContainer<ICreatorColumnAction>> _columnOverrides =
            new List<ColumnToActionContainer<ICreatorColumnAction>>();

        public CreateColumnBuilder(ColumnActionFactory columnActionFactory)
        {
            _columnActionFactory = columnActionFactory;
        }

        public CreateColumnBuilder<T> AddIgnore(string columnName)
        {
            var aOverride = new ColumnToActionContainer<ICreatorColumnAction>
            {
                ColumnName = columnName,
                MatchAction = new IgnoreAction(columnName)
            };

            _columnOverrides.Add(aOverride);
            return this;
        }

        public CreateColumnBuilder<T> AddSymbolic(string columnName, string symbolic)
        {
            var action = _columnActionFactory.GetAction<ICreatorColumnAction>(typeof(T), symbolic);

            var aOverride = new ColumnToActionContainer<ICreatorColumnAction>
            {
                ColumnName = columnName,
                MatchAction = action
            };

            _columnOverrides.Add(aOverride);

            return this;
        }

        public List<ColumnToActionContainer<ICreatorColumnAction>> Out => _columnOverrides;
    }
}
