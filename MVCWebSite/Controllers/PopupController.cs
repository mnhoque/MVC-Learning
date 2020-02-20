using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWebSite.Controllers
{
    public class PopupController : Controller
    {
        // GET: Popup
        public ActionResult PopUp()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            if (Session["UserId"] != null)//user has a login
            {
                return View();
            }
            else
            {

                return RedirectToAction("PopUp");
            }

        }
        public ActionResult CountEachLetter()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CountEachLetter")]
        public ActionResult PostCountEachLetter()
        {
            string sentences = Request["paragraph"];
            string lowerSentences = sentences.ToLower();
            string[] stringArray = { "a", "b", "c", "d", "e", "f", "g", "h",
            "i", "j", "k", "l","m", "n", "o", "p","q", "r", "s", "t","u", "v", "w", "x","y", "z"};
            List<AlphabetFrequency> myList = new List<AlphabetFrequency>();
            int totalcharinlowersentences = 0;

            for (int i = 0; i <= lowerSentences.Length - 1; i++)
            {
                if (lowerSentences[i] >= 'a' && lowerSentences[i] <= 'z')
                {
                    totalcharinlowersentences = totalcharinlowersentences + 1;
                }
            }

            for (int i = 0; i <= stringArray.Length - 1; i++)
            {
                int letterCount = 0;

                for (int j = 0; j <= lowerSentences.Length - 1; j++)
                {

                    if (stringArray[i] == lowerSentences[j].ToString())
                    {
                        letterCount++;
                    }

                }

                AlphabetFrequency alphabetObject = new AlphabetFrequency();
                alphabetObject.alphabet = stringArray[i][0];
                alphabetObject.frequency = letterCount;
                alphabetObject.percent = ((alphabetObject.frequency * 100.00) / totalcharinlowersentences);
                myList.Add(alphabetObject);


            }


            // myList.Sort();

           AlphabetFrequency[] alphabets= myList.ToArray();

            AlphabetFrequency temp;

            // traverse 0 to array length 
            for (int i = 0; i < alphabets.Length - 1; i++)
            
                // traverse i+1 to array length 
                for (int j = i + 1; j < alphabets.Length; j++)
                
                    if (alphabets[i].frequency < alphabets[j].frequency)
                    {
                        temp = alphabets[i];
                        alphabets[i]= alphabets[j];
                        alphabets[j] = temp;
                    }


            // compare array element with  
            // all next element 


            myList = alphabets.ToList();

            

            //myList.Reverse();

            ViewBag.X = myList;

            return View();
        }


    }

    public class AlphabetFrequency : IComparable<AlphabetFrequency>
    {
        public char alphabet;
        public int frequency;
        public double percent;

        public int CompareTo(AlphabetFrequency obj)
        {
            AlphabetFrequency second = (AlphabetFrequency)obj;
            return this.frequency - second.frequency;
        }
    }




}