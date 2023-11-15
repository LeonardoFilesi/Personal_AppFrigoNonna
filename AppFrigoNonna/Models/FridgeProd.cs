using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppFrigoNonna.Models
{
    public class FridgeProd
    {
        [Key] public int Id { get; set; }

        // NOME
        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio")] // aggiunta per la VALIDAZIONE
        [MaxLength(50, ErrorMessage = "Lunghezza massima di 50 CARATTERI")] // aggiunta per la VALIDAZIONE
        public string Name { get; set; }

        
        
        // DESCRIZIONE
        [Column(TypeName = "text")]  // AGGIUNTA se si vogliono specificare ancora di più le colonne
        public string? Description { get; set; }


        
        // IMMAGINE URL
        [Url(ErrorMessage = "Devi inserire un link valido per l'immagine del prodotto")] // aggiunta per la VALIDAZIONE
        [MaxLength(500, ErrorMessage = "Il link non può essere più lungo di 500 caratteri")] // aggiunta per la VALIDAZIONE
        public string? ImgUrl { get; set; }



        // FREEZER
        [Required(ErrorMessage ="Bisogna necessariamente specificare se il prodotto va conservato in frigo o in freezer")]
        public bool Freezer { get; set; }

        // eliminare FREEZER e AGGIUNGERE METODO DI CONSERVAZIONE magari un integer, senza necessariamente scrivere una stringa




        // RELAZIONE N:N con le Categorie
        public List<Category>? Categories { get; set; }


        //======================================COSTRUTTORE========================================
        public FridgeProd(string name, string description, string imgUrl, bool freezer)
        {
            this.Name = name;
            this.Description = description;
            this.ImgUrl = imgUrl;
            this.Freezer = freezer;
        }
        public FridgeProd() { }  // SEMPRE aggiungere costruttore VUOTO
    }
}
