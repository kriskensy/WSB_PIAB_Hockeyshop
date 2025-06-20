using Hockeyshop.Data.Data.Orders;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class InvoiceDocument : IDocument
{
    private readonly Invoice _invoice;
    public InvoiceDocument(Invoice invoice) => _invoice = invoice;

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(10));

            //header
            page.Header().Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("BestHockey").Bold().FontSize(18).FontColor(Colors.Blue.Medium);
                });
                row.RelativeItem().AlignRight().Column(col =>
                {
                    col.Item().Text($"Invoice number: {_invoice.InvoiceNumber}").Bold();
                    col.Item().Text($"Issue date: {_invoice.IssueDate:dd.MM.yyyy}");
                    col.Item().Text($"Order number: {_invoice.IdOrder}");
                });
            });

            //content
            page.Content().Column(column =>
            {
                column.Item().AlignCenter().Text("INVOICE").FontSize(20).Bold().FontColor(Colors.Blue.Medium);

                column.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("SELLER").Bold().FontColor(Colors.Blue.Medium);
                        col.Item().Text("BestHockey\nExample St. 1\n00-000 Warsaw\nVAT: 1234567890");
                    });
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("BUYER").Bold().FontColor(Colors.Blue.Medium);
                        col.Item().Text($"{_invoice.User?.FirstName ?? ""} {_invoice.User?.LastName ?? ""}\nUser ID: {_invoice.IdUser}");
                    });
                });

                //ordered items
                column.Item().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25); //#
                        columns.RelativeColumn(3);  //Product name
                        columns.ConstantColumn(50); //Quantity
                        columns.ConstantColumn(70); //Unit price
                        columns.ConstantColumn(70); //Total
                    });

                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("#");
                        header.Cell().Element(CellStyle).Text("Product");
                        header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                        header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                        header.Cell().Element(CellStyle).AlignRight().Text("Total");
                    });

                    int index = 1;
                    foreach (var item in _invoice.Order.OrderItems)
                    {
                        decimal total = item.UnitPrice * item.Quantity;
                        table.Cell().Element(CellStyle).Text(index.ToString());
                        table.Cell().Element(CellStyle).Text(item.Product?.Name ?? "Product");
                        table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity.ToString());
                        table.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPrice:C2}");
                        table.Cell().Element(CellStyle).AlignRight().Text($"{total:C2}");
                        index++;
                    }

                    //styling
                    static IContainer CellStyle(IContainer container) =>
                        container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                });

                column.Item().PaddingTop(15).AlignRight().Text(text =>
                {
                    text.Span("TOTAL AMOUNT DUE: ").Bold();
                    text.Span($"{_invoice.TotalAmount:C2}").FontSize(14).Bold().FontColor(Colors.Blue.Medium);
                });
            });

            page.Footer().AlignCenter().Text("Thank you for shopping at BestHockey!").FontSize(10).FontColor(Colors.Grey.Darken2);
        });
    }
}
