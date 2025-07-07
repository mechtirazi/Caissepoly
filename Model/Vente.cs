using CaissePoly.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Vente
{
    [Key]
    public int IdV { get; set; }
    public int IdA { get; set; }
    [ForeignKey("IdA")]
    public int Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }
    public virtual Article Article { get; set; }

    public decimal Total => Quantite * PrixUnitaire;
}
