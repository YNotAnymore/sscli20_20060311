// ==++==
//
//   
//    Copyright (c) 2006 Microsoft Corporation.  All rights reserved.
//   
//    The use and distribution terms for this software are contained in the file
//    named license.txt, which can be found in the root of this distribution.
//    By using this software in any fashion, you are agreeing to be bound by the
//    terms of this license.
//   
//    You must not remove this notice, or any other, from this software.
//   
//
// ==--==
using System.IO;
using System.Text;  
using System;
using System.Collections;
using System.Collections.Specialized;
using GenStrings;
public class Co8733get_AllKeys
{
    public static String s_strActiveBugNums = "";
    public static String s_strDtTmVer       = "";
    public static String s_strClassMethod   = "NameValueCollection.AllKeys";
    public static String s_strTFName        = "Co8733get_AllKeys.cs";
    public static String s_strTFAbbrev      = s_strTFName.Substring(0, 6);
    public static String s_strTFPath        = Environment.CurrentDirectory;
    public const int MAX_LEN = 50;          
    public virtual bool runTest()
    {
        Console.WriteLine(s_strTFPath + " " + s_strTFName + " , for " + s_strClassMethod + " , Source ver: " + s_strDtTmVer);
        int iCountErrors = 0;
        int iCountTestcases = 0;
        IntlStrings intl;
        String strLoc = "Loc_000oo";
        NameValueCollection nvc; 
        string [] values = 
        {
            "",
            " ",
            "a",
            "aA",
            "text",
            "     SPaces",
            "1",
            "$%^#",
            "2222222222222222222222222",
            System.DateTime.Today.ToString(),
            Int32.MaxValue.ToString()
        };
        string [] keys = 
        {
            "zero",
            "oNe",
            " ",
            "",
            "aa",
            "1",
            System.DateTime.Today.ToString(),
            "$%^#",
            Int32.MaxValue.ToString(),
            "     spaces",
            "2222222222222222222222222"
        };
        int cnt = 0;            
        string [] ks;          
        try
        {
            intl = new IntlStrings(); 
            Console.WriteLine("--- create collection ---");
            strLoc = "Loc_001oo"; 
            iCountTestcases++;
            nvc = new NameValueCollection();
            Console.WriteLine("1. AllKeys on empty collection");
            if (nvc.Count > 0 )
                nvc.Clear();
            ks = nvc.AllKeys;
            if (ks.Length != 0) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0001, number of keys is {0} instead of 0", ks.Length);
            } 
            Console.WriteLine("2. AllKeys on collection with simple strings");
            strLoc = "Loc_002oo"; 
            for (int i = 0; i < values.Length; i++) 
            {
                iCountTestcases++;
                cnt = nvc.Count;
                nvc.Add(keys[i], values[i]);
                if (nvc.Count != cnt+1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0002_{0}a, count is {1} instead of {2}", i, nvc.Count, cnt+1);
                } 
                iCountTestcases++;
                if (nvc.AllKeys.Length != cnt+1 ) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0002_{0}b, incorrects Keys array", i);
                } 
                iCountTestcases++;
                if (Array.IndexOf(nvc.AllKeys, keys[i]) < 0) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0002_{0}c, collection doesn't contain key of new item", i);
                } 
            }
            Console.WriteLine("3. AllKeys on collection with intl strings");
            int len = values.Length;
            string [] intlValues = new string [len * 2];
            for (int i = 0; i < len * 2; i++) 
            {
                string val = intl.GetString(MAX_LEN, true, true, true);
                while (Array.IndexOf(intlValues, val) != -1 )
                    val = intl.GetString(MAX_LEN, true, true, true);
                intlValues[i] = val;
            } 
            Boolean caseInsensitive = false;
            for (int i = 0; i < len * 2; i++) 
            {
                if(intlValues[i].Length!=0 && intlValues[i].ToLower()==intlValues[i].ToUpper())
                    caseInsensitive = true;
            }
            Console.WriteLine(" initial number of items: " + nvc.Count);
            strLoc = "Loc_003oo"; 
            for (int i = 0; i < len; i++) 
            {
                iCountTestcases++;
                cnt = nvc.Count;
                nvc.Add(intlValues[i+len], intlValues[i]);
                if (nvc.Count != cnt+1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0003_{0}a, count is {1} instead of {2}", i, nvc.Count, cnt+1);
                } 
                iCountTestcases++;
                if (nvc.AllKeys.Length != cnt+1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0003_{0}b, wrong keys array", i);
                } 
                iCountTestcases++;
                if (Array.IndexOf(nvc.AllKeys, intlValues[i+len]) < 0) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0003_{0}c, Array doesn't contain key of new item", i);
                } 
            }
            Console.WriteLine("4. Case sensitivity");
            string [] intlValuesLower = new string [len * 2];
            for (int i = 0; i < len * 2; i++) 
            {
                intlValues[i] = intlValues[i].ToUpper();
            }
            for (int i = 0; i < len * 2; i++) 
            {
                intlValuesLower[i] = intlValues[i].ToLower();
            } 
            nvc.Clear();
            Console.WriteLine(" initial number of items: " + nvc.Count);
            strLoc = "Loc_004oo"; 
            for (int i = 0; i < len; i++) 
            {
                iCountTestcases++;
                cnt = nvc.Count;
                nvc.Add(intlValues[i+len], intlValues[i]);
                if (nvc.Count != cnt+1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0004_{0}a, count is {1} instead of {2}", i, nvc.Count, cnt+1);
                } 
                iCountTestcases++;
                if (nvc.AllKeys.Length != cnt+1 ) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0004_{0}b, wrong keys array", i);
                } 
                iCountTestcases++;
                if (Array.IndexOf(nvc.AllKeys, intlValues[i+len]) < 0) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0004_{0}c, collection doesn't contain key of new item", i);
                } 
                iCountTestcases++;
                if (!caseInsensitive && Array.IndexOf(nvc.AllKeys, intlValuesLower[i+len]) >= 0) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0004_{0}d, key was converted to lower", i);
                }
            }
            Console.WriteLine("5. AllKeys for multiple strings with the same key");
            nvc.Clear();
            len = values.Length;
            string k = "keykey";
            iCountTestcases++;
            for (int i = 0; i < len; i++) 
            {
                nvc.Add(k, "Value"+i);
                iCountTestcases++;
                if (nvc.Count != 1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0005_{1}_a, count is {0} instead of 1", nvc.Count, i);
                } 
                iCountTestcases++;
                if (nvc.AllKeys.Length != 1) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0005_{1}_b, AllKeys contains {0} instead of 1", nvc.AllKeys.Length, i);
                } 
                iCountTestcases++;
                if (Array.IndexOf(nvc.AllKeys, k) != 0 ) 
                {
                    iCountErrors++;
                    Console.WriteLine("Err_0005_{0}_c, wrong key", i);
                } 
            }
            string [] vals = nvc.GetValues(k);
            if (vals.Length != len) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0005d, number of values at given key is {0} instead of {1}", vals.Length, 1);
            }
            Console.WriteLine("6. AllKeys when collection has null-value ");
            strLoc = "Loc_006oo"; 
            iCountTestcases++;
            k = "kk";
            nvc.Remove(k);      
            cnt = nvc.Count;
            Console.WriteLine(" initial number of items: " + cnt);
            nvc.Add(k, null);
            iCountTestcases++;
            if (nvc.Count != cnt+1) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0006a, count is {0} instead of {1}", nvc.Count, cnt+1);
            } 
            iCountTestcases++;
            if (Array.IndexOf(nvc.AllKeys, k) < 0) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0006b, collection doesn't contain key of new item");
            } 
            iCountTestcases++;
            if (nvc[k] != null) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0006c, returned non-null on place of null");
            }
            Console.WriteLine("7. AllKeys when item with null-key is present ");
            Console.WriteLine(" initial number of items: " + nvc.Count);
            strLoc = "Loc_007oo"; 
            nvc.Remove(null);
            cnt = nvc.Count;
            Console.WriteLine(" - add item with null key with no such item previously");
            iCountTestcases++;
            nvc.Add(null, "item");
            iCountTestcases++;
            if (nvc.Count != cnt+1) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0007a, count is {0} instead of {1}", nvc.Count, cnt+1);
            } 
            iCountTestcases++;
            if (Array.IndexOf(nvc.AllKeys, null) < 0) 
            {
                iCountErrors++;
                Console.WriteLine("Err_0007b, collection doesn't contain null key ");
            } 
            iCountTestcases++;
            if (nvc[null] != "item") 
            {
                iCountErrors++;
                Console.WriteLine("Err_0007c, returned wrong value at null key");
            }
        } 
        catch (Exception exc_general ) 
        {
            ++iCountErrors;
            Console.WriteLine (s_strTFAbbrev + " : Error Err_general!  strLoc=="+ strLoc +", exc_general==\n"+exc_general.ToString());
        }
        if ( iCountErrors == 0 )
        {
            Console.WriteLine( "Pass.   "+s_strTFPath +" "+s_strTFName+" ,iCountTestcases=="+iCountTestcases);
            return true;
        }
        else
        {
            Console.WriteLine("Fail!   "+s_strTFPath+" "+s_strTFName+" ,iCountErrors=="+iCountErrors+" , BugNums?: "+s_strActiveBugNums );
            return false;
        }
    }
    public static void Main(String[] args)
    {
        bool bResult = false;
        Co8733get_AllKeys cbA = new Co8733get_AllKeys();
        try 
        {
            bResult = cbA.runTest();
        } 
        catch (Exception exc_main)
        {
            bResult = false;
            Console.WriteLine(s_strTFAbbrev + " : Fail! Error Err_main! Uncaught Exception in main(), exc_main=="+exc_main);
        }
        if (!bResult)
        {
            Console.WriteLine ("Path: "+s_strTFName + s_strTFPath);
            Console.WriteLine( " " );
            Console.WriteLine( "Fail!  "+ s_strTFAbbrev);
            Console.WriteLine( " " );
        }
        if (bResult) Environment.ExitCode=0; else Environment.ExitCode=1;
    }
}
