using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatleshipLibrary.Models;

namespace BatalhaNaval
{
    class Program
    {
        static List<Pino> listaDePinos = new List<Pino>();

        static void Main(string[] args)
        {
            // Criar as 25 posicoes e escrever na tela

            CriarPinos();
            DesenharTabuleiro();

            ColocarNavio();
            // Vai colocar 5 navios !
            Console.Clear();
            DesenharTabuleiro();

            ListarPinosComNavios();

            while (VerificarSeAindaTemNavios())
            {
                DesenharTabuleiro();
                Atirar();
                ListarPinosComNavios();
            }

            Console.WriteLine("GAME OVER !!!");

            Console.ReadLine();
        }

        public static void CriarPinos()
        {
            CriarFileira("A");
            CriarFileira("B");
            CriarFileira("C");
            CriarFileira("D");
            CriarFileira("E");
        }
        public static void CriarFileira(string posicao)
        {
            for (int i = 1; i <= 5; i++)
            {
                Pino estePino = new Pino();
                estePino.Position = $"{posicao}{i}";
                listaDePinos.Add(estePino);
            }
        }

        public static void DesenharTabuleiro()
        {
            int contaPino = 0;
            foreach (Pino estePino in listaDePinos)
            {
                if (estePino.ShipSunk == true)
                {
                    Console.Write($" ** ");
                    contaPino++;
                } 
                else
                {
                    Console.Write($" {estePino.Position} ");
                    contaPino++;
                }

                if (contaPino % 5 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        public static void ColocarNavio()
        {
            int navios = 0;
            while (navios < 5)
            {
                Console.WriteLine($"Pode voce colocar 5 navios ! Você já colocou {navios}");
                Console.WriteLine("Em qual posicao voce quer colocar um navio ?");
                string resposta = Console.ReadLine();

                bool achouNavio = ControlarNavio(resposta.ToUpper());

                if (achouNavio)
                {
                    navios++;
                }
                else
                {
                    Console.WriteLine("Nenhum navio encontrado com esse nome !");
                }
            }
        }
        public static bool ControlarNavio(string pinoPedido)
        {
            foreach (var estePino in listaDePinos)
            {
                if (estePino.Position == pinoPedido)
                {
                    Console.WriteLine($"O objeto {estePino.Position} foi encontrado !");

                    if (estePino.ShipPresent == true)
                    {
                        Console.WriteLine("Já tem navio nessa posição caralho !");
                        return false;
                    }
                    else
                    {
                        estePino.ShipPresent = true;
                        return true;
                    }
                }
            }
            return false;
        }

        public static void ListarPinosComNavios()
        {
            foreach (var pino in listaDePinos)
            {
                if (pino.ShipPresent == true && pino.ShipSunk == false)
                {
                    Console.WriteLine(pino.Position);
                }
            }
        }

        public static bool VerificarSeAindaTemNavios()
        {
            foreach (var pino in listaDePinos)
            {
                if (pino.ShipPresent == true && pino.ShipSunk == false)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Atirar()
        {
            Console.WriteLine("Aonde você quer atirar ?");
            string tiro = Console.ReadLine();

            foreach (var pino in listaDePinos)
            {
                if (tiro.ToUpper() == pino.Position)
                {
                    switch (pino.ShipPresent)
                    {
                        case true:
                            if (pino.ShipSunk)
                            {
                                Console.WriteLine("Você já atirou nesse spot, seu retardado !");
                                break;
                            }
                            Console.WriteLine("ACERTOU !!!");
                            pino.ShipSunk = true;
                            break;
                        default:
                            Console.WriteLine("ERROU");
                            break;
                    }
                }
            }
        }
    }
}
