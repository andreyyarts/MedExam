﻿using System.Collections.Generic;
using System.Windows.Documents;
using MedExam.Common.Interfaces;
using MedExam.Patient.Reports.Views;
using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class BloodTestRpgaForTyphoidReport : IReportFlow
    {
        private readonly ReportService _reportService;
        private long[] _patientIds;

        public BloodTestRpgaForTyphoidReport(ReportService reportService)
        {
            _reportService = reportService;
        }

        public void SetItems(long[] itemIds)
        {
            _patientIds = itemIds;
        }

        public string Title
        {
            get { return "Анализ крови: РПГА на брюшной тиф"; }
        }

        public int CountInWidth
        {
            get { return 2; }
        }

        public int CountInHeight
        {
            get { return 2; }
        }

        public Block Report
        {
            get
            {
                var view = new BloodTestRpgaForTyphoidReportView { Title = Title };
                return view.ReportBlock;
            }
        }

        public IEnumerable<object> Datas
        {
            get
            {
                if (_patientIds == null || _patientIds.Length == 0)
                    return new object[0];

                return _reportService.GetDirectionInImmunologyLaboratoryReportData(_patientIds);
            }
        }
    }
}