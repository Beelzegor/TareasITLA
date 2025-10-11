
// Tirso Mejia Garcia - 20250988
class Program
{
    static void Main()
    {
        Console.Write("Ingrese un número: ");

        int numero = Convert.ToInt32(Console.ReadLine());

        if (numero % 2 == 0)
        {
            Console.WriteLine("El número es par.");
        }
        else
        {
            Console.WriteLine("El número es impar.");
        }
    }
}
