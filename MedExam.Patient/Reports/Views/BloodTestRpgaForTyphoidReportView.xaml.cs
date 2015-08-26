﻿using MedExam.Common.Interfaces;

namespace MedExam.Patient.Reports.Views
{
    /// <summary>
    /// Interaction logic for BloodTestRpgaForTyphoidReportView.xaml
    /// </summary>
    public partial class BloodTestRpgaForTyphoidReportView : IReportView
    {
        public BloodTestRpgaForTyphoidReportView()
        {
            InitializeComponent();
        }

        public string Title { get; set; }
    }
}
