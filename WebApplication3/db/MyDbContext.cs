using System.Data.Entity;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

public class QimenLog
{
    [Key]
    public long ID { get; set; }
   
    public string BillNo { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string Data { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string Response { get; set; }


    [Column(TypeName = "nvarchar(max)")]
    public string QueryString { get; set; }

    public string HttpMethod { get; set; }

    public string MethodName { get; set; }

    /// <summary>
    /// 0 默认，1 为取消。
    /// </summary>
    public int status { get; set; }

    /// <summary>
    /// 1发货，2退货
    /// </summary>
    public int BillType { get; set; }
 
    public DateTime? AddDTM { get; set; }
    
    public DateTime? ModifyDTM { get; set; }

}


public class MyDbContext : DbContext
{
    public MyDbContext(string nameOrConnectionString) :base(nameOrConnectionString)
    {

    }

    public MyDbContext()  
        :this(System.Configuration.ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString)
    {
 
    }


    public DbSet<QimenLog> QimenLogs { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // 可以在这里配置实体类与数据库表之间的映射关系
        modelBuilder.Entity<QimenLog>().ToTable("QimenLogs");
    }
}
