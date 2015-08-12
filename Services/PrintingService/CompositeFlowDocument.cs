using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using MedExam.Common.interfaces;

namespace PrintingService
{
    public class CompositeFlowDocument : FlowDocument
    {
        private const double WidthA4 = 793.75748031496073;
        private const double HeightA4 = 1122.5574803149607;

        public CompositeFlowDocument(IEnumerable<IReportFlow> reports)
        {
            PageHeight = HeightA4;
            PageWidth = WidthA4;
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
                return block;
            });
            return blocks;
        }
    }
}