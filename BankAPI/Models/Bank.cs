using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models;

public class Bank
{   
    //asi se crea la relacion 1 a varios
    //luego en el constructor se crea un HashSet del tipo de la clase
    public Bank(){
        this.Accounts = new HashSet<Account>();
    }

    [Key]
    public string bankCode {get; set;}
    public string name {get; set;}
    public string adress { get; set; }

    //relacion entre entity
    public virtual ICollection<Account> Accounts {get; set;}
}