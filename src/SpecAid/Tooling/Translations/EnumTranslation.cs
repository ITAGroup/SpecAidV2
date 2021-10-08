﻿using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class EnumTranslation : Translation
    {
        public EnumTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return Enum.Parse(info.PropertyType, tableValue, true);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return info.PropertyType.IsEnum;
        }

        public override int ConsiderOrder => TranslationOrder.Enum.ToInt32();
    }
}
