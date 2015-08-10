﻿using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Input;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports;
using MedExam.Patient.Reports.ViewModels;
using Microsoft.Practices.Prism.Commands;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        private readonly IPrintService _printService;
        private ICommand _printReports;

        public ReportListViewModel(IPrintService printService, long[] items)
            : base(items)
        {
            _printService = printService;
            Reports = new List<ReportViewModel>(new[]
            {
                new ReportViewModel("Patient")
            }).ToArray();
        }

        public ReportListViewModel() : base(new long[0]) { }

        public ICommand PrintReports
        {
            get { return _printReports ?? (_printReports = new DelegateCommand(OnPrintReports)); }
        }

        public ReportViewModel[] Reports { get; set; }

        private void OnPrintReports()
        {
            _printService.PrintDocument(new[]
            {
                new DirectionInImmunologyLaboratoryReport(new DirectionInImmunologyLaboratoryReportViewModel
                {
                    CurrentOrganizationName = "ГБУЗ ТО &#171;ОКБ №1&#187;",
                    CurrentDepartmentName = "ОТДЕЛЕНИЕ ПРОФОСМОТРОВ"
                })
            });
        }
    }
}