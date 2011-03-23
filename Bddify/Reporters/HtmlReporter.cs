using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Bddify.Core;
using RazorEngine;

namespace Bddify.Reporters
{
    public class HtmlReporter : IProcessor
    {
        static readonly List<Scenario> Scenarios = new List<Scenario>();

        public ProcessType ProcessType
        {
            get { return ProcessType.Report; }
        }

        static HtmlReporter()
        {
            var report = Razor.Parse(HtmlTemplate.Value, Scenarios);
            File.WriteAllText(FileName, report);
        }

        public void Process(Scenario scenario)
        {
            // ToDo: this is a dirty hack because I am creating the file each and every time.
            // should create the file once and dynamically edit it for consequent calls
            Scenarios.Add(scenario);
            var report = Razor.Parse(HtmlTemplate.Value, Scenarios);
            File.WriteAllText(FileName, report);
        }

        static readonly Lazy<string> HtmlTemplate = new Lazy<string>(GetHtmlTemplate);
        private static readonly string FileName = Path.Combine(AssemblyDirectory, "bddify.html");

        static string GetHtmlTemplate()
        {
            string htmlTemplate;
            var templateResourceStream = typeof(HtmlReporter).Assembly.GetManifestResourceStream("Bddify.Reporters.HtmlReport.cshtml");
            using (var sr = new StreamReader(templateResourceStream))
            {
                htmlTemplate = sr.ReadToEnd();
            }

            return htmlTemplate;
        }

        // http://stackoverflow.com/questions/52797/c-how-do-i-get-the-path-of-the-assembly-the-code-is-in#answer-283917
        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}