﻿using System;
using System.Linq;
using OData2Poco.Extensions;


namespace OData2Poco.CommandLine
{
    public class OptionManager
    {
        private Options PocoOptions { get; set; }       
        public OptionManager(Options options) 
        {
            PocoOptions = options;
            PocoOptions.Validate();

        }

        public PocoSetting GetPocoSetting()
        {
            return new PocoSetting
            {
                AddNavigation = PocoOptions.Navigation,
                AddNullableDataType = PocoOptions.AddNullableDataType,
                AddEager = PocoOptions.Eager,
                Inherit = string.IsNullOrWhiteSpace(PocoOptions.Inherit) ? null : PocoOptions.Inherit,
                NamespacePrefix = string.IsNullOrEmpty(PocoOptions.Namespace) ? string.Empty : PocoOptions.Namespace,
                NameCase = PocoOptions.NameCase.ToCaseEnum(),
                Attributes = PocoOptions.Attributes?.ToList(),
                //obsolete
                AddKeyAttribute = PocoOptions.Key,
                AddTableAttribute = PocoOptions.Table,
                AddRequiredAttribute = PocoOptions.Required,
                AddJsonAttribute = PocoOptions.AddJsonAttribute,
            };
        }

        public OdataConnectionString GetOdataConnectionString()
        {
            var connString = new OdataConnectionString
            {
                ServiceUrl = PocoOptions.Url,
                UserName = PocoOptions.User,
                Password = PocoOptions.Password,
                TokenUrl = PocoOptions.TokenEndpoint,
                TokenParams = PocoOptions.TokenParams,
                ParamFile = PocoOptions.ParamFile,
                //Authenticate = Options.Authenticate,
            };
            Enum.TryParse(PocoOptions.Authenticate, out AuthenticationType auth);
            connString.Authenticate = auth;
            return connString;
        }

        public static implicit operator Options(OptionManager optionManager)
        {
            return optionManager.PocoOptions;
        }
    }
}
