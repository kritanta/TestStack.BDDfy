﻿using System.Collections.Generic;
using TestStack.BDDfy.Core;

namespace TestStack.BDDfy.Processors.Reporters.Html
{
    public class HtmlReportViewModel : FileReportModel
    {
        public HtmlReportViewModel(IHtmlReportConfiguration configuration, IEnumerable<Story> stories)
            : base(stories)
        {
            Configuration = configuration;
        }

        public string CustomStylesheet { get; set; }
        public string CustomJavascript { get; set; }

        public IHtmlReportConfiguration Configuration { get; private set; }
    }
}