import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import java.util.Map.Entry;
import java.util.stream.IntStream;

/**
 * 
 * Genetic optimization class
 *
 */
public class GeneticOptimization {

	/* GA parameters */

	private static final double mutationRate = 1.5;

	private static final int tournamentSize = 3;

	private static final int elitism = 5;

	/** one GA iteration */
	public static Population evolvePopulation(Population pop) {

		// creating population
		Population newPopulation = new Population(pop.size());
		
		// saving elitism
		for (int i = 0; i < elitism; i++) {
			newPopulation.saveShedule(i, pop.getFittest(i));
		}

		// obtaining new individuals
		for (int j = elitism; j < 10; j++) {
			Schedule indiv1 = tournamentSelection(pop);
			Schedule indiv2 = tournamentSelection(pop);
			Schedule newIndiv = crossoverAndMutate(indiv1, indiv2);
			newIndiv.createScheduleAfterMutation();
			newPopulation.saveShedule(j, newIndiv);
		}

		// sorting by target function
		Collections.sort(newPopulation.getShedules(), Schedule.Comparators.TARGET);
		return newPopulation;
	}

	/**
	 */
	private static Schedule crossoverAndMutate(Schedule indiv1, Schedule indiv2) {

		List<Task> tasks = indiv1.getTasks();
		int numberOfWorkers = indiv1.getWorkers().size();
		int numberOfWorkers2 = indiv2.getWorkers().size();
		
		Map<Worker, ArrayList<Task>> workerTasks = new TreeMap<Worker, ArrayList<Task>>();
		Map<Task, ArrayList<Worker>> taskWorkers = new TreeMap<Task, ArrayList<Worker>>();
		for (Worker worker : indiv1.getWorkers()) {
			workerTasks.put(worker, new ArrayList<Task>());
		}

		// create crossover between indices (p1 and p2)
		int minVal = 20;
		int p1 = (int) ((indiv1.getTasks().size() - minVal) * Math.random());
		int p2 = (int) (p1 + (int) ((indiv1.getTasks().size() - p1 - minVal) * Math.random()));

		Map<Task, Worker> indiv1TasksWorkers = new TreeMap<Task, Worker>();
		for (Worker worker : indiv1.getWorkers()) {
			for (Task task : indiv1.getWorkerTasks().get(worker)) {
				indiv1TasksWorkers.put(task, worker);
			}
		}
		Map<Task, Worker> indiv2TasksWorkers = new TreeMap<Task, Worker>();
		for (Worker worker : indiv2.getWorkers()) {
			for (Task task : indiv2.getWorkerTasks().get(worker)) {
				indiv2TasksWorkers.put(task, worker);
			}
		}
		// crossover
		for (int i = 0; i < tasks.size(); i++) {
			if (i < p1 || i > p2) {
				Worker worker = indiv1TasksWorkers.get(tasks.get(i));
				if (worker != null) {
					workerTasks.get(worker).add(tasks.get(i));
				} else { // add to random worker
					int workerIndex = (int) (Math.random() * numberOfWorkers);
					worker = indiv1.getWorkers().get(workerIndex);
					workerTasks.get(worker).add(tasks.get(i));
				}
			} else {
				Worker worker = indiv2TasksWorkers.get(tasks.get(i));
				if (worker != null) {
					workerTasks.get(worker).add(tasks.get(i));
				} else {// add to random worker
					int workerIndex = (int) (Math.random() * numberOfWorkers2);
					worker = indiv2.getWorkers().get(workerIndex);
					workerTasks.get(worker).add(tasks.get(i));
				}
			}

		}
		// mutation
		if (Math.random() < mutationRate) {
			for (int i = 0; i < 5; i++) {
				int taskIndex = (int) (Math.random() * indiv1.getTasks().size());
				Worker worker = null;
				for (Entry<Worker, ArrayList<Task>> entry : workerTasks.entrySet()) {
					if (entry.getValue().contains(tasks.get(taskIndex))) {
						worker = entry.getKey();
						break;
					}
				}
				if (worker != null) { //if the task was assigned
					workerTasks.get(worker).remove(tasks.get(taskIndex));
				}
				int workerIndex = (int) (Math.random() * indiv1.getWorkers().size());
				worker = indiv1.getWorkers().get(workerIndex);
				workerTasks.get(worker).add(tasks.get(taskIndex));
			}
		}

		for(List<Task> taskList : workerTasks.values())
		{
			for(int i = 0; i < taskList.size(); i++)
			{
				taskWorkers.put(taskList.get(i), new ArrayList<Worker>());
			}
		}

		for(Entry<Worker, ArrayList<Task>> entry : workerTasks.entrySet())
		{
			Worker worker2 = entry.getKey();
			List<Task> task2 = entry.getValue();

			for(int i = 0; i < task2.size(); i++)
			{
				taskWorkers.get(task2.get(i)).add(worker2);

				if(task2.get(i).getWorkers() > 1)
				{
					if(indiv1.getTaskWorkers().get(task2.get(i)).contains(worker2))
					{
						List<Worker> workerInd = indiv1.getTaskWorkers().get(task2.get(i));
						for(int j = 0; j < workerInd.size(); j++)
						{
							if(!taskWorkers.get(task2.get(i)).contains(workerInd.get(j)))
							{
								taskWorkers.get(task2.get(i)).add(workerInd.get(j));
							}
						}
					}
					else if (indiv2.getTaskWorkers().get(task2.get(i)).contains(worker2))
					{
						List<Worker> workerInd = indiv2.getTaskWorkers().get(task2.get(i));
						for(int j = 0; j < workerInd.size(); j++)
						{
							if(!taskWorkers.get(task2.get(i)).contains(workerInd.get(j)))
							{
								taskWorkers.get(task2.get(i)).add(workerInd.get(j));
							}
						}
					}
					else
					{
						if(task2.get(i).getWorkers() == 2)
						{
							Worker workerIndex1 = taskWorkers.get(task2.get(i)).get(0);
							boolean indicator = true;
							Worker worker3 = null;
							while(indicator)
							{
								int workerIndex2 = (int) (Math.random() * indiv1.getWorkers().size());
								worker3 = indiv1.getWorkers().get(workerIndex2);
								if(workerIndex1.getId() != worker3.getId())
								{
									indicator = false;
								}
							}
							taskWorkers.get(task2.get(i)).add(worker3);
						}
						else
						{
							Worker workerIndex1 = taskWorkers.get(task2.get(i)).get(0);
							boolean indicator = true;
							Worker worker3 = null;
							while(indicator)
							{
								int workerIndex2 = (int) (Math.random() * indiv1.getWorkers().size());
								worker3 = indiv1.getWorkers().get(workerIndex2);
								if(workerIndex1.getId() != worker3.getId())
								{
									indicator = false;
								}
							}
							taskWorkers.get(task2.get(i)).add(worker3);

							indicator = true;
							Worker worker4 = null;

							while(indicator)
							{
								int workerIndex2 = (int) (Math.random() * indiv1.getWorkers().size());
								worker4 = indiv1.getWorkers().get(workerIndex2);
								if(workerIndex1.getId() != worker4.getId() && worker3.getId() != worker4.getId())
								{
									indicator = false;
								}
							}
							taskWorkers.get(task2.get(i)).add(worker4);
						}
					}
				}
			}
		}

		Schedule newSol = new Schedule(indiv1.getTasks(), indiv1.getWorkers(), workerTasks, taskWorkers); // CIA PARASIAU PATS NULL ANTRA
		
		return newSol;
	}

	/**
	 * tournament selection
	 */
	private static Schedule tournamentSelection(Population pop) {

		// make tournament population
		Population tournament = new Population(tournamentSize);

		// choose random solution
		for (int i = 0; i < tournamentSize; i++) {
			int randomId = (int) (Math.random() * pop.size());
			tournament.saveShedule(i, pop.getShedule(randomId));
		}

		// get fittest solution from tournament
		Schedule fittest = tournament.getFittest();
		return fittest;
	}

}
