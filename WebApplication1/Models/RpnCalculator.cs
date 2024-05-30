namespace WebApplication1.Models
{

    /*
     * 
Mod�le:

*Pourquoi avoir utiliser List de stack plut�t qu'un dictionnaire ? cela permettrait � l'acc�s � une stack depuis la cl� qui contiendrait l'identifiant unique.
*Au lieu d'avoir des lists on pourrait utiliser des IEnumerable ou des IList, surtout comme le service fait des cast avec .toList() lors de l'utilisation de celles ci.
*Pourquoi ne pas avoir fait une propri�t� opur la liste des stacks?
*Pourquoi avoir utiliser une classe plut�t qu'un record?

     */
    public class RpnCalculator
    {
        public List<Stack<double>> stacks = new List<Stack<double>>();
        public enum Operators 
        {
            Add = '+',
            Substract = '-',
            Multiply = '*',
            Divide = '/',
        }

        public RpnCalculator()
        {
            stacks = new List<Stack<double>>();
        }
    }
}