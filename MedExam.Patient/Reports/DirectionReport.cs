﻿using System.Collections.Generic;
using MedExam.Common;
using MedExam.Common.Interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public sealed class DirectionReport : ReportFlow<IReportData>
    {
        private readonly ReportService _reportService;
        private long[] _patientIds;

        public DirectionReport(ReportService reportService)
        {
            _reportService = reportService;
        }

        public override void SetItems(long[] itemIds)
        {
            _patientIds = itemIds;
        }

        public override string Name { get; set; }
        public override string Title { get; set; }
        public override string Template { get; set; }

        public override int CountInWidth
        {
            get { return 2; }
        }

        public override int CountInHeight
        {
            get { return 2; }
        }

        public override IEnumerable<IReportData> Datas
        {
            get
            {
                if (_patientIds == null || _patientIds.Length == 0)
                    return new DirectionReportViewModel[0];

                return _reportService.GetDirectionReportData(_patientIds);
            }
        }
    }
}