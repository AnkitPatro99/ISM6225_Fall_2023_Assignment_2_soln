/* 
 
YOU ARE NOT ALLOWED TO MODIFY ANY FUNCTION DEFINATION's PROVIDED.
WRITE YOUR CODE IN THE RESPECTIVE QUESTION FUNCTION BLOCK


*/

using System.Text;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -109 <= lower <= upper <= 109
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity:O(1)
        */

        // Method to find missing ranges
        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            IList<IList<int>> result = new List<IList<int>>();
            long prev_val = (long)lower - 1; // use long to avoid overflow

            try
            {
                for (int i = 0; i <= nums.Length; i++)
                {
                    // Calculate the current value. If we reach the end of the array, use the upper bound
                    long curr_val = (i == nums.Length) ? (long)upper + 1 : nums[i];

                    // Check if there is a missing range of size 1
                    if (prev_val + 2 == curr_val)
                    {
                        result.Add(new List<int> { (int)prev_val + 1 });
                    }

                    // Check if there is a missing range of size greater than 1
                    else if (prev_val + 2 < curr_val)
                    {
                        result.Add(new List<int> { (int)prev_val + 1, (int)curr_val - 1 });
                    }

                    // Update the 'prev' value
                    prev_val = curr_val;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine("An exception occurred: " + ex.Message);
            }

            // Return the list containing the missing ranges
            return result;
        }


        /*
         
        Question 2

        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.
 
        Example 1:

        Input: s = "()"
        Output: true
        Example 2:

        Input: s = "()[]{}"
        Output: true
        Example 3:

        Input: s = "(]"
        Output: false

        Constraints:

        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity:O(n^2), space complexity:O(1)
        */

        public static bool IsValid(string input)
        {
            try
            {
                if (input.Length % 2 != 0)
                {
                    return false;
                }

                // It initializes a Stack<char> to keep track of the opening brackets encountered.
                Stack<char> stack = new Stack<char>();
                foreach (char c in input)
                {
                    // If c is an opening bracket, i.e., '(', '{', or '[', it pushes it onto the stack.
                    if (c == '(' || c == '{' || c == '[')
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        if (stack.Count == 0)
                        {
                            return false;
                        }

                        char top_most = stack.Peek();
                        if ((c == ')' && top_most != '(') || (c == '}' && top_most != '{') || (c == ']' && top_most != '['))
                        {
                            return false;
                        }
                        stack.Pop();
                    }
                }

                return stack.Count == 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day.You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.
 
        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity:O(1)
        */

        public static int MaxProfit(int[] rates)
        {
            try
            {
                if (rates.Length < 2) // If the array length is less than 2, it's not possible to make any profit
                {
                    return 0;
                }

                int maxProfit = 0;
                int minRate = rates[0];

                // The method iterates through the rates array starting from the second element (index 1) since the first element has been set
                // as the initial minimum price.
                for (int i = 1; i < rates.Length; i++)
                {
                    if (rates[i] < minRate)
                    {
                        minRate = rates[i];
                    }
                    else
                    {
                        int currentProfit = rates[i] - minRate;
                        if (currentProfit > maxProfit)
                        {
                            maxProfit = currentProfit;
                        }
                    }
                }

                return maxProfit;
            }
            catch (Exception)
            {
                throw;
            }
        }



        /*
        
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number.A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).
        Example 1:

        Input: num = "69"
        Output: true
        Example 2:

        Input: num = "88"
        Output: true
        Example 3:

        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity:O(n), space complexity:O(1)
        */

        public static bool IsStrobogrammatic(string strint)
        {
            try
            {
                // Creating a dictionary to store the mapping of strobogrammatic numbers
                var dict1 = new System.Collections.Generic.Dictionary<char, char>()
                {
                    {'0', '0'},
                    {'1', '1'},
                    {'6', '9'},
                    {'8', '8'},
                    {'9', '6'}
                };

                // Initialize two pointers, l and r
                int l = 0;
                int r = strint.Length - 1;

                // Loop through the string from both ends
                while (l <= r)
                {
                    // If the characters don't match the strobogrammatic mapping, return false
                    if (!dict1.ContainsKey(strint[l]) || dict1[strint[l]] != strint[r])
                    {
                        return false;
                    }
                    l++;
                    r--;
                }

                // If the loop completes, the string is a strobogrammatic number
                return true;
            }
            catch (Exception)
            {
                // Handle any exceptions that might occur here
                throw;
            }
        }

        /*

        Question 5:
        Given an array of integers nums, return the number of good pairs.A pair (i, j) is called good if nums[i] == nums[j] and i < j. 

        Example 1:

        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:

        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array are good.
        Example 3:

        Input: nums = [1,2,3]
        Output: 0

        Constraints:

        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity:O(n), space complexity:O(n)

        */

        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                // It initializes a variable Count_of_goodpairs to keep track of the count of good pairs.
                int Count_of_goodpairs = 0;

                // It also initializes a dictionary freq_dict to keep track of the frequency of each element in the array.
                var freq_dict = new System.Collections.Generic.Dictionary<int, int>();

                foreach (int num in nums)
                {
                    if (freq_dict.ContainsKey(num))
                    {
                        Count_of_goodpairs += freq_dict[num];
                        freq_dict[num]++;
                    }
                    else
                    {
                        freq_dict[num] = 1;
                    }
                }

                return Count_of_goodpairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        Question 6

        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:

        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:

        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:

        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.
        Constraints:

        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity:O(nlogn), space complexity:O(n)
        */

        public static int ThirdMax(int[] nums)
        {
            try
            {
                // Sorting the array in descending order
                Array.Sort(nums);
                Array.Reverse(nums);

                // Initializing uniqueCount and the third maximum
                int uniqueCount = 1;
                int thirdMax = nums[0];

                for (int i = 1; i < nums.Length; i++)
                {
                    // Checking if the current element is distinct
                    if (nums[i] != nums[i - 1])
                    {
                        uniqueCount++;
                    }

                    // If the third distinct maximum is found, update thirdMax and exit the loop
                    if (uniqueCount == 3)
                    {
                        thirdMax = nums[i];
                        break;
                    }
                }

                return thirdMax;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        
        Question 7:

        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].
        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:

        Input: currentState = "+"
        Output: []
 
        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                // Initialize a list to store resulting moves
                List<string> result = new List<string>();

                // If the length of the input is less than 2, return an empty list
                if (currentState.Length < 2)
                {
                    return result;
                }

                char[] stateArray = currentState.ToCharArray();
                for (int i = 0; i < currentState.Length - 1; i++)
                {
                    if (stateArray[i] == '+' && stateArray[i + 1] == '+')
                    {
                        // Flip the consecutive '++' to '--'
                        stateArray[i] = '-';
                        stateArray[i + 1] = '-';

                        // Add the modified string to the result list
                        result.Add(new string(stateArray));

                        // Revert the changes for the next iteration
                        stateArray[i] = '+';
                        stateArray[i + 1] = '+';
                    }
                }
                // Return the list of possible moves
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 8:

        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.
        Example 1:

        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"

        Example 2:

        Input: s = "aeiou"
        Output: ""

        Timecomplexity:O(n), Space complexity:O(n)
        */

        private static string RemoveVowels(string s)
        {
            try
            {
                // write your code here
                String final_string = "";
                //Writing for loop for taking input
                for (int i = 0; i < s.Length; i++)
                {
                    //this condition will check and compare each element with given below vowels
                    if (s[i] == 'a' || s[i] == 'e' || s[i] == 'i' || s[i] == 'o' || s[i] == 'u' ||
                        s[i] == 'A' || s[i] == 'E' || s[i] == 'I' || s[i] == 'O' || s[i] == 'U')
                    {

                    }

                    else
                    {
                        final_string = final_string + s[i];
                    }

                }
                return final_string;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}
