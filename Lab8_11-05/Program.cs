using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semana8_KG
{
    class Program
    {
        public static DataClasses1DataContext context = new DataClasses1DataContext();

        static void Main(string[] args)
        {
            Console.WriteLine("----------------INTO TO LINQ:-----\n");
            IntroToLINQ();
            Console.WriteLine("\n--------------DATASOURCE:----------------\n");
            DataSource();
            Console.WriteLine("\n--------------Filtering:-----------------\n");
            filtering();
            Console.WriteLine("\n--------------Ordering:------------------\n");
            Ordering();
            Console.WriteLine("\n--------------Grouping:------------------\n");
            Grouping();
            Console.WriteLine("\n--------------Grouping2:-----------------\n");
            Grouping2();
            Console.WriteLine("\n--------------Joining:-------------------\n");
            Joining();

            Console.Read();
        }

        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;


            //Lambda 
            var numberQuery = numbers.Where(n => (n % 2 == 0)).ToList();

            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void DataSource()
        {
            var _queryAllCustomers = from cust in context.clientes select cust;

            //Lambda
            var queryAllCustomers = context.clientes.Select(cust => cust).ToList();

            foreach (var item in queryAllCustomers)

            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void filtering()
        {

            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }

        }
        static void Ordering()
        {
            var queryLondonCustomers3 = from cust in context.clientes
                                        where cust.Ciudad == "London"
                                        orderby cust.NombreCompañia ascending
                                        select cust;

            //Lambda
            var queryLondonCustomers = context.clientes.Where(cust => cust.Ciudad == "Londres").ToList();

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }
        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            //Lambda
            var queryLondonCustomers3 = context.clientes.Where(cust => cust.Ciudad == "Londres").OrderBy(x => x.NombreCompañia).ToList();

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine("{0}", customer.NombreCompañia);
                }
            }

        }
        static void Grouping2()
        {
            var _custQuery = from cust in context.clientes
                             group cust by cust.Ciudad into custGroup
                             where custGroup.Count() > 2
                             orderby custGroup.Key
                             select custGroup;

            //Lambda
            var custQuery = context.clientes.GroupBy(c => c.Ciudad).Where(x => x.Count() > 2).OrderBy(y => y.Key).ToList();


            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

        }
        static void Joining()
        {
            var _innerJoinQuery = from cust in context.clientes
                                  join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                                  select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            //Lambda
            var innerJoinQuery = context.clientes.
                Join(context.Pedidos, cust => cust.idCliente,
dist => dist.IdCliente, (cust, dist) => new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario }).ToList();


            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
    

