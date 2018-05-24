using Rock.BackgroundErrorLogging;
using Rock.Logging.Diagnostics;
using Rock.StaticDependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Rock.Logging.Rock.StaticDependencyInjection
{
    internal partial class CompositionRoot : CompositionRootBase
    {
        public override void Bootstrap()
        {
            ImportMultiple<IContextProvider>(x => DefaultContextProviders.SetCurrent(x as IList<IContextProvider> ?? x.ToList()));
            ImportFirst<ILogFormatter>(EmailLogProvider.SetDefaultLogFormatter, "EmailLogFormatter");
            ImportFirst<ILogFormatter>(ConsoleLogProvider.SetDefaultLogFormatter, "ConsoleLogFormatter");
            ImportFirst<ILogFormatter>(FileLogProvider.SetDefaultLogFormatter, "FileLogFormatter");
            ImportFirst<ILogEntryFactory>(DefaultLogEntryFactory.SetCurrent);
            ImportFirst<ILoggerFactory>(LoggerFactory.SetCurrent);
            ImportFirst<IStepLoggerFactory>(DefaultStepLoggerFactory.SetCurrent);
            ImportFirst<IXmlNamespaceProvider>(LogEntryExtendedProperties.SetXmlNamespace);
        }

        protected override void OnError(string message, Exception exception, ImportInfo import)
        {
            BackgroundErrorLogger.Log(exception, "Static Dependency Injection - " + message, "Rock.Logging", "ImportInfo:\r\n" + import);

            base.OnError(message, exception, import);
        }

        /// <summary>
        /// Gets a value indicating whether static dependency injection is enabled.
        /// </summary>
        public override bool IsEnabled
        {
            get
            {
                const string key = "Rock.StaticDependencyInjection.Enabled";
                var enabledValue = ConfigurationManager.AppSettings.Get(key) ?? "true";
                return enabledValue.ToLower() != "false";
            }
        }

        /// <summary>
        /// Return a collection of metadata objects that describe the export operations for a type.
        /// </summary>
        /// <param name="type">The type to get export metadata.</param>
        /// <returns>A collection of metadata objects that describe export operations.</returns>
        protected override IEnumerable<ExportInfo> GetExportInfos(Type type)
        {
            var attributes = Attribute.GetCustomAttributes(type, typeof(ExportAttribute));

            if (attributes.Length == 0)
            {
                return base.GetExportInfos(type);
            }

            return
                attributes.Cast<ExportAttribute>()
                .Select(attribute =>
                    new ExportInfo(type, attribute.Priority)
                    {
                        Disabled = attribute.Disabled,
                        Name = attribute.Name
                    });
        }
    }
}
