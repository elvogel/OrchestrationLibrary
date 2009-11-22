using System;
using System.Collections.Generic;
using System.Text;

namespace EndpointSystems.OrchestrationLibrary
{
    public struct DateTimeRange
    {
        public DateTime From;
        public DateTime To ;
    }

    class OrchestrationReport
    {

        private DateTimeRange _range;
        private string _outDir;
        private string _title;

        public DateTimeRange ReportRange
        {
            get { return _range; }
            set { _range = value; }
        }

        public string OutputDirectory
        {
            get { return _outDir; }
            set { _outDir = value; }
        }

        public string ReportTitle
        {
            get { return _title; }
            set { _title = value; }
        }


    }
}
