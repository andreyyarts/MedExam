using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using MedExam.Common;
using MedExam.Common.interfaces;

namespace PrintingService
{
    public class CompositionReportsOnFlowDocument : FlowDocument
    {
        public CompositionReportsOnFlowDocument(IEnumerable<IReportFlow> reports)
        {
            IsHyphenationEnabled = true;
            IsOptimalParagraphEnabled = true;

            PageHeight = FormatSizes.A4.Height;
            PageWidth = FormatSizes.A4.Width;
            PagePadding = new Thickness(0);

            var paragraph = new Paragraph
            {
                Padding = new Thickness(0),
                Margin = new Thickness(0)
            };

            paragraph.Inlines.AddRange(reports
                                    .OrderBy(r => r.CountInWidth).ThenBy(r => r.CountInHeight)
                                    .SelectMany(DataToInline));

            Blocks.Add(paragraph);
        }

        private IEnumerable<Figure> DataToInline(IReportFlow report)
        {
            return GetBlocksWithFillDatas(report).Select(block => new Figure(block)
            {
                HorizontalAnchor = FigureHorizontalAnchor.PageLeft,
                Width = new FigureLength(Math.Truncate(PageWidth) / report.CountInWidth),
                Height = new FigureLength(Math.Truncate(PageHeight) / report.CountInHeight),
                Padding = new Thickness(0),
                Margin = new Thickness(0)
            });
        }

        private static IEnumerable<Block> GetBlocksWithFillDatas(IReportFlow report)
        {
            var blocks = report.Datas.Select(data =>
            {
                var block = report.Report;
                block.DataContext = data;
                block.Margin = GetDoubleThickness(block.Margin);
                block.Padding = GetDoubleThickness(block.Padding);
                return block;
            });
            return blocks;
        }

        private static Thickness GetDoubleThickness(Thickness thickness)
        {
            const int delta = 2;
            return new Thickness(thickness.Left * delta,
                thickness.Top * delta,
                thickness.Right * delta,
                thickness.Bottom * delta);
        }
    }
}