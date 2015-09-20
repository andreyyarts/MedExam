﻿using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class DirectionInBacteriologicalLaboratoryReport : DirectionReport
    {
        private const string Template = "Направление в бактериологическую лабораторию";

        public DirectionInBacteriologicalLaboratoryReport(ReportService reportService)
            : base(reportService, Template)
        {
        }

        public override string Title
        {
            get { return Template; }
        }
    }
}