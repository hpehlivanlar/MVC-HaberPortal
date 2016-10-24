namespace HaberSistemi.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tablokur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Haber",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 255),
                        KisaAciklama = c.String(),
                        Aciklama = c.String(),
                        Okunma = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                        Resim = c.String(maxLength: 255),
                        KategoriID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kategori", t => t.KategoriID, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID, cascadeDelete: true)
                .Index(t => t.KullaniciID)
                .Index(t => t.KategoriID);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 150),
                        ParentID = c.Int(nullable: false),
                        URL = c.String(maxLength: 150),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(maxLength: 150),
                        Telefon = c.String(nullable: false, maxLength: 16),
                        Email = c.String(nullable: false),
                        Sifre = c.String(nullable: false, maxLength: 8),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                        Rol_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rol", t => t.Rol_ID)
                .Index(t => t.Rol_ID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RolAdi = c.String(maxLength: 150),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimUrl = c.String(),
                        HaberID = c.Int(nullable: false),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Haber", t => t.HaberID, cascadeDelete: true)
                .Index(t => t.HaberID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resim", "HaberID", "dbo.Haber");
            DropForeignKey("dbo.Haber", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Kullanici", "Rol_ID", "dbo.Rol");
            DropForeignKey("dbo.Haber", "KategoriID", "dbo.Kategori");
            DropIndex("dbo.Resim", new[] { "HaberID" });
            DropIndex("dbo.Kullanici", new[] { "Rol_ID" });
            DropIndex("dbo.Haber", new[] { "KategoriID" });
            DropIndex("dbo.Haber", new[] { "KullaniciID" });
            DropTable("dbo.Resim");
            DropTable("dbo.Rol");
            DropTable("dbo.Kullanici");
            DropTable("dbo.Kategori");
            DropTable("dbo.Haber");
        }
    }
}
