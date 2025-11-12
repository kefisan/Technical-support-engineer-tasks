namespace ColorsAlgorithm;

public class ColorsTaskTests
{

  private ColorsTask _colorsTask;

  [SetUp]
  public void Setup()
  {
    _colorsTask = new ColorsTask();
  }

  [Test]
  public void CountColorTransitions_AllHedgehogsAlreadyInTargetColor_ReturnsZero()
  {
    Assert.That(_colorsTask.CountColorTransitions(0, [5, 0, 0]), Is.EqualTo(ColorsTask.NoTransitionNeeded));
    Assert.That(_colorsTask.CountColorTransitions(1, [0, 10, 0]), Is.EqualTo(ColorsTask.NoTransitionNeeded));
    Assert.That(_colorsTask.CountColorTransitions(2, [0, 0, 20]), Is.EqualTo(ColorsTask.NoTransitionNeeded));
  }

  [Test]
  public void CountColorTransitions_OnlyTheNonTargetColorPresent_ReturnsImpossible()
  {
    Assert.That(_colorsTask.CountColorTransitions(0, [0, 4, 0]), Is.EqualTo(ColorsTask.ImpossibleTransition));
    Assert.That(_colorsTask.CountColorTransitions(1, [7, 0, 0]), Is.EqualTo(ColorsTask.ImpossibleTransition));
  }

  [Test]
  public void CountColorTransitions_TwoNonTargetColorsEqual_ReturnsEqualNumber()
  {
    Assert.That(_colorsTask.CountColorTransitions(0, new int[] { 0, 5, 5 }), Is.EqualTo(5));
    Assert.That(_colorsTask.CountColorTransitions(2, new int[] { 3, 7, 7 }), Is.EqualTo(7));
  }

  [Test]
  public void CountColorTransitions_EvenDifferenceBetweenNonTargetColors_ReturnsPossibleTransition()
  {
    Assert.That(_colorsTask.CountColorTransitions(0, [1, 6, 4]), Is.EqualTo(6));
    Assert.That(_colorsTask.CountColorTransitions(1, [8, 1, 4]), Is.EqualTo(8));
  }

  [Test]
  public void CountColorTransitions_OddDifferenceBetweenNonTargetColors_ReturnsImpossible()
  {
    Assert.That(_colorsTask.CountColorTransitions(2, [1, 4, 2]), Is.EqualTo(ColorsTask.ImpossibleTransition));
  }

  [Test]
  public void CountColorTransitions_NoHedgehogsOfAnyColor_ReturnsNoTransitionNeeded()
  {
    Assert.That(_colorsTask.CountColorTransitions(2, [0, 0, 0]), Is.EqualTo(ColorsTask.NoTransitionNeeded));
  }

  [Test]
  public void CountColorTransitions_LargeNumbers_ReturnsCorrectResult()
  {
    Assert.That(_colorsTask.CountColorTransitions(1, [1073741824, 1073741823, 1073741823]), Is.EqualTo(ColorsTask.ImpossibleTransition));
  }

  [Test]
  public void CountColorTransitions_OneNonTargetColorZeroAndTargetColorZero_ReturnsImpossible()
  {
    Assert.That(_colorsTask.CountColorTransitions(2, [1, 7, 0]), Is.EqualTo(ColorsTask.ImpossibleTransition));
    Assert.That(_colorsTask.CountColorTransitions(0, [0, 3, 0]), Is.EqualTo(ColorsTask.ImpossibleTransition));
    Assert.That(_colorsTask.CountColorTransitions(1, [0, 0, 5]), Is.EqualTo(ColorsTask.ImpossibleTransition));
  }

  [Test]
  public void CountColorTransitions_ExecutionTime_CompletesWithinTimeLimit()
  {
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    _colorsTask.CountColorTransitions(0, [0, int.MaxValue, int.MaxValue]);
    stopwatch.Stop();

    Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(ColorsTask.MaxExecutionTimeSeconds));
  }

  [Test]
  public void CountColorTransitions_MemoryUsage_StaysUnder100MB()
  {
    long initialMemory = GC.GetTotalMemory(true);

    _colorsTask.CountColorTransitions(0, [0, int.MaxValue, int.MaxValue]);

    long finalMemory = GC.GetTotalMemory(false);
    long memoryUsedBytes = finalMemory - initialMemory;
    long memoryUsedMb = memoryUsedBytes / (1024 * 1024);

    Assert.That(memoryUsedMb, Is.LessThan(100),
      $"Memory usage was {memoryUsedMb} MB, which exceeds the 100 MB limit");
  }
}