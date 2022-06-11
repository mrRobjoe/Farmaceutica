using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmaceutica
{
    internal class Program
    {
        static String[] codigo = new string[10];//variable global
        static String[] descripcion = new string[10];//variable global
        static float[] precio = new float[10];//variable global
        static byte indice;
        static void Main(string[] args)
        {
            Menu();
        }

        public static void Menu()
        {
            char opcion = ' '; //variable local

            do
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("A.Inicializar arreglos ");
                Console.WriteLine("B.Agregar");//Tarea(opcional), conusltar si el código que se ingresa existe o no
                Console.WriteLine("C.Consultar");
                Console.WriteLine("D.Borrar");//Tarea, consultar y poner los detalles en blanco
                Console.WriteLine("E.Modificar");//Tarea, consultar código, si existe, modificar todo lo demás excepto el código
                Console.WriteLine("F.Sub-Reportes");//Tarea,
                                                //subreportes(submenú) opciones:
                                                //(1-todos los medicamentos(Reportes en inventario)
                                                //2-promedio precios
                                                //3-Medicamento más caro y más barato)
                Console.WriteLine("G.Salir");
                Console.WriteLine("---------------------------");
                Console.WriteLine("Digite una opción");
                opcion = char.Parse(Console.ReadLine().ToUpper());

                switch (opcion)
                {
                    case 'A':
                        inicializar(); 
                        break;
                    case 'B':
                        Agregar();
                        break;
                    case 'C':
                        Consultar(SolicitarCodigo());//pasar por parámetro solicitar código. 
                        break;
                    case 'D':
                        Borrar(SolicitarCodigo());//pasar por parámetro solicitar código.
                        break;
                    case 'E':
                        Modificar(SolicitarCodigo());//pasar por parámetro solicitar código.
                        break;
                    case 'F':
                        SubReporte();
                        break;
                    case 'G':
                        break;
                    default:
                        Console.WriteLine("La opcion digitada es incorrecta");
                        break;
                }
            } while (!opcion.Equals('G'));

        }

        //método privado para inicializar las variables.
        private static void inicializar()
        {
            indice = 0;
            for (int i = 0; i < 10; i++)
            {
                codigo[i] = "";
                descripcion[i] = "";
                precio[i] = 0;
            }
            Console.WriteLine("Arreglos inicializados");
        }

        //método privado para mostrar submenú de reportes. 
        private static void SubReporte()
        {
            String opcionesMenu="-----Sub-Reportes-----\n";
            opcionesMenu += "1.Reporte de inventario.\n";
            opcionesMenu += "2.Promedio precios.\n";
            opcionesMenu += "3.Medicamento más caro y más barato.\n";
            opcionesMenu += "4.Salir.\n";
            opcionesMenu += "--------------------------";
            opcionesMenu += "Digite una opción.";
            int opcion;

            do
            {
                Console.WriteLine(opcionesMenu);
                opcion= int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Reporte();
                        break;
                    case 2:
                        PromedioPrecios();
                        break;
                    case 3:
                        MedicamentoCaroYBarato();
                        break;
                    case 4:
                        break;
                }

            } while (opcion!=4); 
        }

        //MÉTODOS DE SUBMENÚ.

        //Método de opción 1 del submenú.
        private static void Reporte()//probar imprimir mientras ses distinto a " " o 0
        {
            Console.WriteLine("Codigo      Descripcion         Precio");
            Console.WriteLine("--------------------------------------");
            for (int i = 0; i < indice; i++)
            {
                if (!codigo[i].Equals(""))
                {
                    Console.WriteLine($"{codigo[i]}          {descripcion[i]}            {precio[i]}");
                }
            }
        }
        //Método de opción 2 del submenú.
        private static void PromedioPrecios()
        {
            float resultado = 0;
            
            for (int i = 0; i < precio.Length; i++)
            {
                resultado += precio[i];
            }
            resultado /= indice;
            Console.WriteLine($"El promedio de precios es de {resultado}");
        }
        //Método de opción 3 del submenú.
        private static void MedicamentoCaroYBarato()
        {
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");

            string descripcionMayor="";
            float precioMayor = precio[0];

            float precioMenor = precio[0];
            string descripcionMenor="";
            for (int i = 0; i < indice; i++) /*faltan detalles por completar en la busqueda del mayor y el menor,
                                               esto debido a que me lanza ceros y errores de cálculo
                                               del mayor y el menor en ocasiones.*/
            {
                   
               if ((precio[i] > precioMayor))
               {                 
                    precioMayor=precio[i];
                    descripcionMayor = descripcion[i];

               }
               else if ((precio[i] < precioMenor) && (precio[i]!=0))
                {
                    precioMenor=precio[i];
                    descripcionMenor = descripcion[i];
               }
                else if (precioMenor==0)
                {
                    if ((precio[i] < precioMayor))//en caso de que precioMenor sea 0, comprara con precioMayor
                    {
                        precioMenor = precio[i];
                        descripcionMenor = descripcion[i];
                    }
                }
            }
            Console.WriteLine($"Precio mayor: {precioMayor}, {descripcionMayor}, Precio menor: {precioMenor}, {descripcionMenor}");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
        }



        //MÉTODOS DE MENÚ PRINCIPAL, CONTINUACIÓN.

        //método privado para agregar los datos del medicamento en el inventario.
        private static void Agregar()
        {
            char opcion = ' ';
            try
            {
                do
                {
                    Console.WriteLine("Digite el código: ");
                    codigo[indice] = Console.ReadLine();
                    bool bandera = false;
                    for (int i = 0; i < indice; i++)
                    {
                        if (codigo[indice].Equals(codigo[i]))//verificar si se repite el código.
                        {
                            bandera = true;
                        }
                    }
                    if (bandera==false)//si la bandera queda en su forma original no se repite.
                    {
                        Console.WriteLine("Digite la descripcion: ");
                        descripcion[indice] = Console.ReadLine();
                        Console.WriteLine("Digite el precio: ");
                        precio[indice] = float.Parse(Console.ReadLine());
                        indice++;
                        Console.WriteLine("Desea continuar? (S/N)");
                        opcion = Convert.ToChar(Console.ReadLine().ToUpper());
                    }
                    else//si no, se repite y no puede continuar con los otros datos.
                    {
                        Console.WriteLine("El código ingresado ya existe, desea volver a intentarlo? (S/N).");
                        opcion = Convert.ToChar(Console.ReadLine().ToUpper());
                    }
                } while (!opcion.Equals('N'));
            }
            catch (Exception)
            {
                Console.WriteLine("Ya no puede agregar más medicamentos.");             
            }
            finally
            {
                Console.WriteLine("Final");
            }
        }

        //procedimiento, se le pedirá al user un código, para no tener que repetir o borrar
        private static string SolicitarCodigo()
        {
            Console.WriteLine("Ingrese el código");
            string cod = Console.ReadLine();

            return cod;
        }
        //procedimiento privado para consultar un medicamento, se busca si existe, si existe se muestra la información.
        private static void Consultar(String codigo1)
        {
            int i = 0;
            /*mientras i sea menor e igual al indice y el código solicitado
             sea distinto a los códigos en la lista, aumentar i.*/
            while ((i <= indice) && (!codigo1.Equals(codigo[i]))) 
            {
                i++;
            }
            if (i > indice)
            {
                Console.WriteLine("Código no existe");
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine($"El medicamento es: {descripcion[i]} y el precio es: {precio[i]}");
            }
        }

        //procedimiento privado para borrar los datos de un medicamento, primero se solicita el código y se comprueba si existe.
        private static void Borrar(String codig)
        {
            int i = 0;
            /*mientras i sea menor e igual al indice y el código solicitado
              sea distinto a los códigos en la lista, aumentar i.*/
            while ((i <= indice) && (!codig.Equals(codigo[i])))
            {
                i++;             
            }
            if (i > indice)
            {
                Console.WriteLine("Código inexistente");
            }
            else
            {              
                codigo[i]=""; 
                descripcion[i]="";
                precio[i] =0 ;
            }
        }

        /*procedimiento privado para modificar la información de un medicamento a excepción del código, se solicita el código y
        se comprueba si existe.*/
        private static void Modificar(String code)
        {
            int i = 0;
            /*mientras i sea menor e igual al indice y el código solicitado
             sea distinto a los códigos en la lista, aumentar i.*/
            while ((i<=indice) && (!code.Equals(codigo[i])))
            {
                i++;
            }
            if (i > indice)
            {
                Console.WriteLine("Código inexistente");
            }
            else
            {
                Console.WriteLine("Digite la descripcion: ");
                descripcion[i] = Console.ReadLine();
                Console.WriteLine("Digite el precio: ");
                precio[i] = float.Parse(Console.ReadLine());
            }
        }
    }
}
