using CaissePoly.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using CaissePoly.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Vente
{
    [Key]
    public int IdV { get; set; }

    // ✅ Clé étrangère vers Ticket
    public int IdT { get; set; }

    // ✅ Clé étrangère vers Article
    public int IdA { get; set; }

    public int Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    // ✅ Navigation properties
    [ForeignKey("IdA")]
    public virtual Article Article { get; set; }

    [ForeignKey("IdT")]
    public virtual Ticket Ticket { get; set; }

    public decimal Total => Quantite * PrixUnitaire;
}

