using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.ColumnActions;

namespace SpecAid.Tooling
{
    public class FieldAid
    {
        private readonly ColumnActionFactory _columnActionFactory;

        public FieldAid(ColumnActionFactory columnActionFactory)
        {
            _columnActionFactory = columnActionFactory;
        }

        public TField Create<TField>(string value)
        {
            var objectField = new ObjectField<TField>();

            var matchAction = _columnActionFactory.GetAction<ICreatorColumnAction>(typeof(ObjectField<TField>), "field");

            matchAction.GoGoCreateColumnAction(objectField, value);

            return objectField.Field;
        }

        public void Compare<T>(T actual, string expected)
        {
            var objectField = new ObjectField<T>
            {
                Field = actual
            };

            var matchAction = _columnActionFactory.GetAction<IComparerColumnAction>(typeof(ObjectField<T>), "field");

            var assertResult = matchAction.GoGoCompareColumnAction(objectField, expected);

            Console.WriteLine("Actual: ");
            Console.WriteLine("\t" + assertResult.ActualPrint);
            Console.WriteLine("Expected: ");
            Console.WriteLine("\t" + assertResult.ExpectedPrint);
            Console.WriteLine("Messages: ");

            if (assertResult.IsError)
            {
                Assert.Fail(assertResult.ErrorMessage);
            }
            else
            {
                Console.WriteLine("\t" + "none");
            }
        }
    }
}
