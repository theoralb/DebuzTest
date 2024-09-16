#include <iostream> // for cin cout
int main()
{
    //input   dividend , divisor , decimalPlace
    int dividend, divisor, decimalPlace;
    std::cout << "Enter the dividend: ";
    std::cin >> dividend;
    std::cout << "Enter the divisor: ";
    std::cin >> divisor;
    std::cout << "Enter the decimal place: ";
    std::cin >> decimalPlace;

  
    if (divisor != 0)  //protected math undefine for division
    {

        //Long divide step 1  
        dividend = dividend % divisor; 
        int targetDecimal = 0;

        //Long divide step 2  
        for (int i = 0; i < decimalPlace; ++i)
        {
            dividend *= 10; //add 0 after mod
            targetDecimal = dividend / divisor;//get current number after point
            dividend = dividend % divisor; 
        }

        std::cout << "The digit at the " << decimalPlace << " decimal place is: " << targetDecimal << std::endl;
        // BigO => O(n) 
    }
    else
    {
        std::cout << "Error: Division by zero is not allowed." << std::endl;
    }
    return 0;
}
