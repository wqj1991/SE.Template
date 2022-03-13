using SE.Infrastructure.Stores;
using SE.MyProjectName.Domain.Shared.Books;
using SqlSugar;

namespace SE.MyProjectName.Infrastructure.Books;

[SugarTable("Book")]
public class BookStore: Store<long>
{
    [SugarColumn(ColumnName = "Id", IsPrimaryKey = true, IsIdentity = true)]
    public override long Id { get; protected set; }

    [SugarColumn(ColumnName = "Name")]
    public string Name { get; set; }

    [SugarColumn(ColumnName = "Type")]
    public BookType Type { get; set; }

    [SugarColumn(ColumnName = "PublishDate")]
    public DateTime PublishDate { get; set; }

    [SugarColumn(ColumnName = "Price")]
    public Decimal Price { get; set; }
}