
Console.WriteLine("Mi Agenda Perrón");
Console.WriteLine("Bienvenido a tu lista de contactes");

Agenda agenda = new Agenda();
bool running = true;

while (running)
{
    Console.Write("1. Agregar Contacto      ");
    Console.Write("2. Ver Contactos     ");
    Console.Write("3. Buscar Contactos      ");
    Console.Write("4. Modificar Contacto        ");
    Console.Write("5. Eliminar Contacto     ");
    Console.WriteLine("6. Salir");
    Console.Write("Elige una opción: ");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            agenda.AddContact();
            break;
        case 2:
            agenda.ViewContacts();
            break;
        case 3:
            agenda.SearchContact();
            break;
        case 4:
            agenda.EditContact();
            break;
        case 5:
            agenda.DeleteContact();
            break;
        case 6:
            running = false;
            break;
        default:
            Console.WriteLine("Opción no válida");
            break;
    }
}

// Clase Contact
class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    public Contact(int id, string name, string phone, string email, string address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }
}

// Clase Agenda
class Agenda
{
    private List<Contact> contacts = new List<Contact>();
    private int nextId = 1;

    public void AddContact()
    {
        Console.WriteLine("Vamos a agregar ese contacte que te trae loco.");

        Console.Write("Digite el Nombre: ");
        string name = Console.ReadLine()!;

        Console.Write("Digite el Teléfono: ");
        string phone = Console.ReadLine()!;

        Console.Write("Digite el Email: ");
        string email = Console.ReadLine()!;

        Console.Write("Digite la dirección: ");
        string address = Console.ReadLine()!;

        contacts.Add(new Contact(nextId, name, phone, email, address));
        nextId++;

        Console.WriteLine("Contacto agregado correctamente.\n");
    }

    public void ViewContacts()
    {
        Console.WriteLine("Id           Nombre          Telefono            Email           Dirección");
        Console.WriteLine("___________________________________________________________________________");

        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.Id}    {contact.Name}      {contact.Phone}      {contact.Email}     {contact.Address}");
        }
        Console.WriteLine();
    }

    public void SearchContact()
    {
        Console.WriteLine("Digite un Id de Contacto Para Mostrar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        Contact contact = contacts.Find(c => c.Id == idSeleccionado)!;
        if (contact != null)
        {
            Console.WriteLine($"Nombre: {contact.Name}");
            Console.WriteLine($"Teléfono: {contact.Phone}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Dirección: {contact.Address}");
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
        }
    }

    public void EditContact()
    {
        ViewContacts();
        Console.WriteLine("Digite un Id de Contacto Para Editar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        Contact contact = contacts.Find(c => c.Id == idSeleccionado)!;
        if (contact != null)
        {
            Console.Write($"El nombre es: {contact.Name}, Digite el Nuevo Nombre: ");
            contact.Name = Console.ReadLine()!;

            Console.Write($"El Teléfono es: {contact.Phone}, Digite el Nuevo Teléfono: ");
            contact.Phone = Console.ReadLine()!;

            Console.Write($"El Email es: {contact.Email}, Digite el Nuevo Email: ");
            contact.Email = Console.ReadLine()!;

            Console.Write($"La dirección es: {contact.Address}, Digite la nueva dirección: ");
            contact.Address = Console.ReadLine()!;

            Console.WriteLine("Contacto modificado correctamente.\n");
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
        }
    }

    public void DeleteContact()
    {
        ViewContacts();
        Console.WriteLine("Digite un Id de Contacto Para Eliminar");
        int idSeleccionado = Convert.ToInt32(Console.ReadLine());

        Contact contact = contacts.Find(c => c.Id == idSeleccionado)!;
        if (contact != null)
        {
            Console.WriteLine("Seguro que desea eliminar? 1. Si, 2. No");
            int opcion = Convert.ToInt32(Console.ReadLine());
            if (opcion == 1)
            {
                contacts.Remove(contact);
                Console.WriteLine("Contacto eliminado correctamente.\n");
            }
        }
        else
        {
            Console.WriteLine("Contacto no encontrado.");
        }
    }
}
