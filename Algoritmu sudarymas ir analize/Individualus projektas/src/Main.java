import java.io.IOException;
import java.text.ParseException;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.stream.IntStream;


/**
Code for students, to understand how the Genetic optimization technique can be 
customized for rostering problem. There code is not optimized for the data set 
example presented together. 
 * 
 * Hard constraints (not parametrized):
 * 	MAXIMUM __ HOURS PER DAY
 *  MAX __ WORKING DAYS IN SEQ
 *  FREE DAYS IN SEQ (MIN __)
 *  
 */
public class Main {
		
	public static void main(String[] args) throws IOException, ParseException, InterruptedException, ExecutionException{
		int cnt = 0;
		while(cnt < 1000000)
		{
					IntStream.range(0, 5).parallel().forEach(j ->
		{
			int cnt2 = 0;
			int limit = 4144 * 4144 / 100;
			for(int i = 0; i < limit; i++)
			{
				//System.out.println("lala");
				cnt2++;
			}
		});

			cnt++;
		}

		/*Config.prepareData();

		int workerCount = 100;
		List<Worker> workers = UtilityClass.getWorkers(workerCount);

		List<Task> tasks = UtilityClass.getTasks("data/data3.tsv");

		int totalTime = 0;

		for(Task task : tasks)
		{
			totalTime += task.getDuration();
		}
		System.out.println("Total time: " + totalTime);

		Population newPop = new Population(10, true, tasks, workers);
		
		// Writing results
		System.out.println("Total tasks: " + tasks.size() );
		
		System.out.println("Total workers: " + workers.size() );
		
		Schedule fittest = newPop.getFittest();

		System.out.println("[Not completed minutes: " + fittest.getTargetValue());

		System.out.println("Total skipped tasks (before optimization): " + fittest.getSkipedTaskList().size() );

		//UtilityClass.printResults(fittest, "results");

		// GA OPTIMIZATION START -----------
		int sameIterationCount = 1;
		int lastTarget = newPop.getFittest(0).getTargetValue();
        while (sameIterationCount < 1000) {
        	  
        	  newPop = GeneticOptimization.evolvePopulation(newPop);
        	  
	          Schedule bestShedule = newPop.getFittest();
        	  if(lastTarget != bestShedule.getTargetValue()){
				  sameIterationCount = 1;
				  lastTarget = bestShedule.getTargetValue();
				  fittest = bestShedule;
			  }
			  
        	  //System.out.println("[Not completed minutes: " + bestShedule.getTargetValue() +  ", not assigned tasks: " + bestShedule.getSkipedTaskList().size() + "] Same iteration count: " + sameIterationCount + " target value: " + bestShedule.getTargetValue() + " mean: " + newPop.getMean());
 			  sameIterationCount++;
		 
		}
    	
    	System.out.println("Total skiped tasks (after optimization): " + fittest.getSkipedTaskList().size() );
    	// GA OPTIMIZATION END -----------
		
		// printed results
		UtilityClass.printResults(fittest, "results");

		List<Task> Tasks = fittest.getTasks();
		List<Task> Skipped = fittest.getSkipedTaskList();
		int one = 0;
		int two = 0;
		int three = 0;
		for(int i = 0; i < Tasks.size(); i++)
		{
			if(Skipped.contains(Tasks.get(i)))
			{
				int nr = Tasks.get(i).getWorkers();
				if(nr == 1)
				{
					one++;
					//System.out.println(tasks.get(i).getId() + " " +  1);
				}
				if(nr == 2)
				{
					two++;
					//System.out.println(tasks.get(i).getId() + " " +  2);
				}
				if(nr == 3)
				{
					three++;
					//System.out.println(tasks.get(i).getId() + " " +  3);
				}
			}
		}

		System.out.println(one);
		System.out.println(two);
		System.out.println(three);*/
    }
   
}