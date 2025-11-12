namespace ColorsAlgorithm;

public class ColorsTask
{
    private const int ColorCount = 3;
    private const int MinColorIndex = 0;
    private const int MaxColorIndex = 2;
    private const int MinPopulation = 0;
    public const double MaxExecutionTimeSeconds = 1.0;
    public const int ImpossibleTransition = -1;
    public const int NoTransitionNeeded = 0;
    private const int EvenDivisor = 2;

    public int CountColorTransitions(int targetColor, int[] population)
    {
        if (population.Length != ColorCount ||
            targetColor < MinColorIndex || targetColor > MaxColorIndex)
        {
            throw new ArgumentException("Invalid input");
        }

        for (int i = 0; i < ColorCount; i++)
        {
            if (population[i] < MinPopulation)
            {
                throw new ArgumentException("Population count must be non-negative");
            }
        }

        int firstOther = population[(targetColor + 1) % ColorCount];
        int secondOther = population[(targetColor + 2) % ColorCount];

        if ((firstOther == MinPopulation && secondOther == MinPopulation))
        {
            return NoTransitionNeeded;
        }

        if ((firstOther == MinPopulation || secondOther == MinPopulation) &&
            (population[targetColor] == MinPopulation))
        {
            return ImpossibleTransition;
        }

        if (firstOther == secondOther)
        {
            return firstOther;
        }

        int smaller = Math.Min(firstOther, secondOther);
        int larger = Math.Max(firstOther, secondOther);
        int difference = larger - smaller;

        if(population[targetColor] + smaller < difference)
        {
            return ImpossibleTransition;
        }

        if (difference % EvenDivisor == 0)
        {
            try
            {
                int resultSum = checked(smaller + difference);
                return resultSum;
            }
            catch (OverflowException)
            {
                return ImpossibleTransition;
            }
        }

        return ImpossibleTransition;
    }
}