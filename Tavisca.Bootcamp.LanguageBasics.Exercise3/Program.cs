using System;
using System.Linq;
using System.Collections;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            //number of items in menu
            int numItems=protein.Length;

            //array to calculate calorie value of menu items
            int[] calories=new int[numItems];

            //to store the index of desired menu item for each diet plan
            int[] result=new int[dietPlans.Length];

            for(int i=0;i<numItems;i++)  //calculating calories
            {
                calories[i]=5*protein[i]+5*carbs[i]+9*fat[i];
            }

            for(int i=0; i<dietPlans.Length; i++)  //iterating the diet plans
            {
                BitArray b=new BitArray(numItems,true); //bit array to store index of desired diet plans

                for(int j=0;j<dietPlans[i].Length;j++)
                {
                    char c=dietPlans[i][j];
                    switch (c)
                    {
                        case 'C':
                            int maxCarb=findMax(b,carbs);
                            b=changeArray(b,maxCarb,carbs);
                            break;
                        case 'c':
                            int minCarb=findMin(b,carbs);
                            b=changeArray(b,minCarb,carbs);
                            break;
                        case 'P':
                            int maxProtein=findMax(b,protein);
                            b=changeArray(b,maxProtein,protein);
                            break;
                        case 'p':
                            int minProtein=findMin(b,protein);
                            b=changeArray(b,minProtein,protein);
                            break;
                        case 'F':
                            int maxFat=findMax(b,fat);
                            b=changeArray(b,maxFat,fat);
                            break;
                        case 'f':
                            int minFat=findMin(b,fat);
                            b=changeArray(b,minFat,fat);
                            break;
                        case 'T':
                            int maxCalorie=findMax(b,calories);
                            b=changeArray(b,maxCalorie,calories);
                            break;
                        case 't':
                            int minCalorie=findMin(b,calories);
                            b=changeArray(b,minCalorie,calories);
                            break;
                        default:
                            Console.WriteLine("Some character other than CcPpFfTt encountered");
                            break;
                    }
                }
                
               
                for(int l=0;l<b.Length;l++)
                {
                    if(b[l]==true)
                    {
                        result[i]=l;
                        break;
                    }

                }   

            }
            return result;
        }
        public static int findMin(BitArray b,int[] array)
        {
            int min=Int32.MaxValue;
            for(int i=0;i<array.Length;i++)
            {
                if(b[i]==true && array[i]<min)
                    min=array[i];
            }
            return min;
        }
        public static int findMax(BitArray b,int[] array)
        {
            int max=Int32.MinValue;
            for(int i=0;i<array.Length;i++)
            {
                if(b[i]==true && array[i]>max)
                    max=array[i];
            }
            return max;
        }

        public static BitArray changeArray(BitArray b,int ideal,int[] nutrient)
        {
            for(int i=0;i<nutrient.Length;i++)
            {
                if(b[i]==true && nutrient[i]==ideal)
                {
                    continue;
                }
                else
                {
                    b[i]=false;
                }
            }
            return b;


        }
    }
}
