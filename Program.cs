using System;
using System.IO;
using System.Xml.Serialization;

namespace CSHARP_DZ
{
    [Serializable]
    public class Van
    {
        public string name { get; set; }
        public int number { get; set; }
        public bool type_pass { get; set; }
        public int places { get; set; }
        public int capacity { get; set; }

        public Van(string n, int num, bool typ, int plac, int cap)
        {
            name = n;
            number = num;
            type_pass = typ;
            places = plac;
            capacity = cap;
        }
        public Van() { }

    }
    [Serializable]
    public class Locomotive
    {

        public string name { get; set; }
        public int number { get; set; }
        public bool type_thermal { get; set; }

        public Locomotive(string n, int num, bool typ)
        {
            name = n;
            number = num;
            type_thermal = typ;
        }
        public Locomotive() { }

    }
    [Serializable]
    public class Structure
    {

        public int number { get; set; }
        public Locomotive loc { get; set; }
        public Van[] van { get; set; }

        public Structure(int num, Locomotive l, Van[] v)
        {
            number = num;
            loc = l;
            van = v;
        }
        public Structure() { }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Van van1 = new Van("sueprVan", 1, true, 123, 4567);
            Van van2 = new Van("ultraVan", 2, true, 333, 111);
            Van[] van = new Van[2];
            van[0] = van1;
            van[1] = van2;
            Locomotive loc = new Locomotive("Tutu", 3, false);
            Structure struc = new Structure(5, loc, van);
            Console.WriteLine("Создали структуру");


            XmlSerializer formatter = new XmlSerializer(typeof(Structure));

            using (FileStream fs = new FileStream("Structure.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, struc);
            }
            Console.WriteLine("Сериализовали");

            using (FileStream fs = new FileStream("Structure.xml", FileMode.OpenOrCreate))
            {
                Structure newstruc = (Structure)formatter.Deserialize(fs);
                Console.WriteLine($"number: {newstruc.number} --- Locomotive name: {newstruc.loc.name} --- van1: {newstruc.van[0].name}");
            }
            Console.WriteLine("Десериализовали");


            Console.ReadKey();
        }
    }

}
