﻿using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class ClinicalTrialReport : DirectionReport
    {
        private const string Template = "Клиническое исследование";

        public ClinicalTrialReport(ReportService reportService)
            : base(reportService, Template)
        {
        }

        public override string Title
        {
            get { return Template; }
        }
    }
}