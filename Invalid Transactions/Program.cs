using System;
using System.Linq;
using System.Collections.Generic;

namespace Invalid_Transactions
{
  class Program
  {
    static void Main(string[] args)
    {
      Solution s = new Solution();
      var transactions = new string[] { "alice,20,800,mtv", "bob,50,100,beijing", "alice,100,800,mtv", "alice,70,100,beijing",
      "alice,200,800,mtv", "bob,120,100,beijing", "bob,170,800,mtv", "bob,70,100,beijing"};
      var answer = s.InvalidTransactions(transactions);
      foreach (var a in answer) Console.WriteLine(a);
    }
  }


  public class Solution
  {
    // Time: O(transactions.length^2), worst case is when all transactions have the same name and amounts are <=1000, for each we iterate through every transaction
    // Space: O(transactions.length), worst case is when all transactions have a unique name so each of them has a separate entry in the map
    public IList<string> InvalidTransactions(string[] transactions)
    {
      // map transaction name to all transactions with that name
      Dictionary<string, List<string[]>> map = new Dictionary<string, List<string[]>>();

      foreach (string currTransaction in transactions)
      {
        string[] splitTransaction = currTransaction.Split(",");

        if (!map.ContainsKey(splitTransaction[0]))    // add list for the name if it doesn't exist
          map.Add(splitTransaction[0], new List<string[]>());

        map[splitTransaction[0]].Add(splitTransaction);     // add current transaction to appropriate list
      }

      List<string> result = new List<string>();

      // every loop checks if the currTransaction is invalid 
      foreach (string currTransaction in transactions)
      {
        string[] currSplitTransaction = currTransaction.Split(",");

        if (int.Parse(currSplitTransaction[2]) > 1000)
        {
          result.Add(currTransaction);

        }
        else
        {

          // iterate through all transactions with the same name, check if within 60 minutes and different city
          foreach (string[] curr in map[currSplitTransaction[0]])
          {

            if (Math.Abs(int.Parse(currSplitTransaction[1]) - int.Parse(curr[1])) <= 60 && !currSplitTransaction[3].Equals(curr[3]))
            {
              result.Add(currTransaction);
              break;
            }
          }
        }
      }

      return result;
    }
  }
}
