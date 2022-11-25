import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collections;
import java.util.Comparator;
import java.util.GregorianCalendar;
import java.util.List;
import java.util.Map;
import java.util.Random;
import java.util.TreeMap;
import java.util.Map.Entry;

public class Schedule {

	private List<Task> tasks;

	private List<Worker> workers;

	private Map<Worker, ArrayList<Task>> workerTasks;
	private Map<Task, ArrayList<Worker>> taskWorkers;

	private List<Task> skipedTaskList = new ArrayList<Task>();

	// constructon to create initial schedule
	Schedule(List<Task> tasks, List<Worker> workers) {
		this.tasks = tasks;
		Collections.sort(this.tasks, Task.Comparators.STARTTIME);
		this.workers = workers;

		workerTasks = new TreeMap<Worker, ArrayList<Task>>();
		for (Worker worker : workers) {
			workerTasks.put(worker, new ArrayList<Task>());
		}

		taskWorkers = new TreeMap<Task, ArrayList<Worker>>();
		for (Task task : tasks) {
			taskWorkers.put(task, new ArrayList<Worker>());
		}

	}
	// constructon to create schedule in genetic optimization stage
	Schedule(List<Task> tasks, List<Worker> workers, Map<Worker, ArrayList<Task>> workerTasks, Map<Task, ArrayList<Worker>> taskWorkers) {
		this.tasks = tasks;
		Collections.sort(this.tasks, Task.Comparators.STARTTIME);
		this.workers = workers;
		this.workerTasks = workerTasks;
		this.taskWorkers = taskWorkers;

		skipedTaskList = new ArrayList<Task>(tasks);
		for (ArrayList<Task> eachWorkerTasks : workerTasks.values()) {
			skipedTaskList.removeAll(eachWorkerTasks);
		}

		for (Worker worker : workers) {
			workerTasks.put(worker, new ArrayList<Task>());
		}
	}

	public void generateShedule() {

		skipedTaskList = new ArrayList<Task>();

		// make random list of tasks
		List<Task> taskList = new ArrayList<Task>(tasks);
		Collections.shuffle(taskList, new Random());
		int numberOfWorkers = workers.size();


		// make list of workers
		List<Worker> workerList = new ArrayList<Worker>(workers);

		// iterating by randimized tasks lists
		for (Task task : taskList) {

			if(task.getWorkers() == 1)
			{
				int workerIndex = (int) (Math.random() * numberOfWorkers);
				Worker worker = workers.get(workerIndex);
				//workerTasks.get(worker).add(task);
				taskWorkers.get(task).add(worker);
			}

			if(task.getWorkers() == 2)
			{
				int workerIndex = (int) (Math.random() * numberOfWorkers);
				Worker worker = workers.get(workerIndex);
				//workerTasks.get(worker).add(task);
				taskWorkers.get(task).add(worker);
				boolean notSame = true;
				int workerIndex2 = 0;
				while(notSame)
				{
					workerIndex2 = (int) (Math.random() * numberOfWorkers);
					if(workerIndex != workerIndex2) notSame = false;
				}

				Worker worker2 = workers.get(workerIndex2);
				//workerTasks.get(worker2).add(task);
				taskWorkers.get(task).add(worker2);
			}

			if(task.getWorkers() == 3)
			{
				int workerIndex = (int) (Math.random() * numberOfWorkers);
				Worker worker = workers.get(workerIndex);
				//workerTasks.get(worker).add(task);
				taskWorkers.get(task).add(worker);
				boolean notSame = true;
				int workerIndex2 = 0;
				while(notSame)
				{
					workerIndex2 = (int) (Math.random() * numberOfWorkers);
					if(workerIndex != workerIndex2) notSame = false;
				}

				Worker worker2 = workers.get(workerIndex2);
				//workerTasks.get(worker2).add(task);
				taskWorkers.get(task).add(worker2);

				notSame = true;
				int workerIndex3 = 0;
				while(notSame)
				{
					workerIndex3 = (int) (Math.random() * numberOfWorkers);
					if(workerIndex != workerIndex3 && workerIndex2 != workerIndex3) notSame = false;
				}

				Worker worker3 = workers.get(workerIndex3);
				//workerTasks.get(worker3).add(task);
				taskWorkers.get(task).add(worker3);
			}
		}
		createScheduleAfterMutation();
	}



	public void createScheduleAfterMutation() {

		for(Task task : tasks)
		{
			ArrayList<Worker> workers = taskWorkers.get(task);

			//if(workers.size() == task.getWorkers() || 1 == 1)
			int passedTasks = 0;
			for(int i = 0; i < task.getWorkers(); i++)
			{
				Worker worker = workers.get(i);
				boolean isBusy = false;

				for(Task workerTask : workerTasks.get(worker))
				{
					if (tasksIntersects(task, workerTask) ) {
						isBusy = true;
						break;
					}
				}

				List<Task> workerTasksTmp = (List<Task>) workerTasks.get(worker).clone();
				workerTasksTmp.add(task);

				if(!workerIsAbleToPerformTask(workerTasksTmp, worker.getMaxHoursPerMonth(), worker)){
					isBusy = true;
				}

				if (isBusy && !skipedTaskList.contains(task)) {
					skipedTaskList.add(task);
				} else {

					passedTasks++;
				}
			}

			if(passedTasks == task.getWorkers())
			{
				for(int i = 0; i < task.getWorkers(); i++)
				{
					Worker worker = workers.get(i);
					workerTasks.get(worker).add(task);
					if(skipedTaskList.contains(task)) {
						skipedTaskList.remove(task);
					}
				}
			}
		}

		for(Worker worker : workerTasks.keySet())
		{
			worker.setAirLineCnt(0);
			worker.setAirline1("");
			worker.setAirline2("");
			worker.setAirline3("");
		}
	}
	// check two tasks interesects
	public static boolean tasksIntersects(Task task1, Task task2) {
		if ((task1.getTaskStartTime() >= task2.getTaskStartTime() && task1.getTaskStartTime() <= task2.getTaskEndTime())
				|| (task1.getTaskEndTime() >= task2.getTaskStartTime() && task1.getTaskEndTime() <= task2.getTaskEndTime())
				|| (task1.getTaskStartTime() >= task2.getTaskStartTime() && task1.getTaskEndTime() <= task2.getTaskEndTime())
				|| (task2.getTaskStartTime() >= task1.getTaskStartTime() && task2.getTaskEndTime() <= task1.getTaskEndTime())) {
			return true;
		} else {
			return false;
		}
	}

	public static boolean workerIsAbleToPerformTask(List<Task> workerTasks, int maxHours, Worker worker) {
		Calendar calendar = GregorianCalendar.getInstance(); // creates a new calendar instance
		Collections.sort(workerTasks, Task.Comparators.STARTTIME);

		int workingDaysInSeq = 0;
		int beforeTaskDay = 0;
		int startTimeInMinutes = 0;
		int totalWorkingTime = 0;
		int totalWorkingTimeAtStart = 0;
		int previousDayTime = 0;
		int taskEndTime = 0;
		int finishTime = 0;
		int startTime = 0;
		boolean isShorter = false;
		boolean hadBreakOne = false;
		boolean hadBreakTwo = false;

		for (Task task : workerTasks){
			calendar = GregorianCalendar.getInstance();
			calendar.setTime(task.getDateFrom());   // assigns calendar to given date
			int minus = 0;
			int totalWorkingMinutesInDay = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - startTimeInMinutes;

			if(hadBreakTwo)
			{
				totalWorkingMinutesInDay -= 120;
			}
			else if(hadBreakOne)
			{
				totalWorkingMinutesInDay -= 60;
			}

			if (calendar.get(calendar.DAY_OF_MONTH) == beforeTaskDay){

				if(worker.getAirLineCnt() == 0)
				{
					worker.setAirLineCnt(1);
					worker.setAirline1(task.getIdAirline());
				}
				else if(worker.getAirLineCnt() == 1)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false)
					{
						worker.setAirLineCnt(2);
						worker.setAirline2(task.getIdAirline());
					}
				}
				else if(worker.getAirLineCnt() == 2)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false && task.getIdAirline().equals(worker.getAirline2()) == false)
					{
						worker.setAirLineCnt(3);
						worker.setAirline3(task.getIdAirline());
					}
				}
				else if(worker.getAirLineCnt() == 3)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false && task.getIdAirline().equals(worker.getAirline2()) == false && task.getIdAirline().equals(worker.getAirline3()) == false)
					{
						return false;
					}
				}

				if (calendar.get(calendar.HOUR_OF_DAY) * 60 > 22 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 < 6 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 + task.getDuration() > 22 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 + task.getDuration() < 6 * 60)
				{
					isShorter = true;
				}

				if(totalWorkingMinutesInDay >= 60 * 4 && hadBreakOne == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) < finishTime + 60)
				{
					return false;
				}
				else if(totalWorkingMinutesInDay >= 60 * 4 && hadBreakOne == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) >= finishTime + 60)
				{
					minus = 60;
					hadBreakOne = true;
				}

				if(totalWorkingMinutesInDay >= 60 * 8  && hadBreakTwo == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) < finishTime + 60)
				{
					return false;
				}
				else if(totalWorkingMinutesInDay >= 60 * 8  && hadBreakTwo == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) >= finishTime + 60)
				{
					minus = 60;
					hadBreakTwo = true;
				}


				previousDayTime += calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - taskEndTime;
				totalWorkingTime += calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - taskEndTime - minus;
				taskEndTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();
				finishTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();
				//totalWorkingTime += task.getDuration();

				if(totalWorkingTime > maxHours * 60) // MAX 164 OR 84 HOURS PER MONTH
				{
					return false;
				}
				if (totalWorkingMinutesInDay > 12*60 && isShorter == false){ // MAXIMUM 12 HOURS PER DAY
					return false;
				}
				else if(totalWorkingMinutesInDay > 8 * 60 && isShorter == true) // MAXIMUM 8 HOURS PER DAY
				{
					return false;
				}
			}else {
				if (calendar.get(calendar.DAY_OF_MONTH) - beforeTaskDay == 1) {
					workingDaysInSeq++;
					if (workingDaysInSeq > 5) {   // MAX 5 WORKING DAYS IN SEQ
						return false;
					}
					beforeTaskDay = calendar.get(calendar.DAY_OF_MONTH);
				} else {
					workingDaysInSeq = 1;
				}

				if (finishTime + 12 * 60 < calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE)) // AT LEAST 12 HOURS SINCE LAST WORK PERIOD
				{
					return false;
				}

				if (previousDayTime < 3 * 60 && previousDayTime > 0) // AT LEAST 3 HOURS PER DAY
				{
					totalWorkingTime = totalWorkingTimeAtStart + 3 * 60;

					if (startTime + 180 + 12 * 60 < calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE)) // AT LEAST 12 HOURS SINCE LAST WORK PERIOD
					{
						return false;
					}
				}

				if(worker.getAirLineCnt() == 0)
				{
					worker.setAirLineCnt(1);
					worker.setAirline1(task.getIdAirline());
				}
				else if(worker.getAirLineCnt() == 1)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false)
					{
						worker.setAirLineCnt(2);
						worker.setAirline2(task.getIdAirline());
					}
				}
				else if(worker.getAirLineCnt() == 2)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false && task.getIdAirline().equals(worker.getAirline2()) == false)
					{
						worker.setAirLineCnt(3);
						worker.setAirline3(task.getIdAirline());
					}
				}
				else if(worker.getAirLineCnt() == 3)
				{
					if(task.getIdAirline().equals(worker.getAirline1()) == false && task.getIdAirline().equals(worker.getAirline2()) == false && task.getIdAirline().equals(worker.getAirline3()) == false)
					{
						return false;
					}
				}

				hadBreakOne = false;
				hadBreakTwo = false;

				startTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE);
				finishTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();


				beforeTaskDay = calendar.get(calendar.DAY_OF_MONTH);
				totalWorkingTimeAtStart = totalWorkingTime;
				totalWorkingTime += task.getDuration();
				previousDayTime = task.getDuration();
				startTimeInMinutes = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE);

				taskEndTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();

				if (calendar.get(calendar.HOUR_OF_DAY) * 60 > 22 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 < 6 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 + task.getDuration() > 22 * 60 || calendar.get(calendar.HOUR_OF_DAY) * 60 + task.getDuration() < 6 * 60)
				{
					isShorter = true;
				}

				if(totalWorkingTime > maxHours * 60)  // MAX 164 OR 84 HOURS PER MONTH
				{
					return false;
				}
			}
		}
		return true;
	}


	public int getTargetValue() {

		int target = 0;

		for (Task task : skipedTaskList) {
			target += task.getDuration();
		}

		for (Worker worker :workers) {
			if(worker.getMaxHoursPerMonth() < totalWorkingTime(this.workerTasks.get(worker))){
				target += totalWorkingTime(this.workerTasks.get(worker)) - worker.getMaxHoursPerMonth();
			}
		}
		return target;
	}


	public  int totalWorkingTime(List<Task> workerTasks) {
		Calendar calendar = GregorianCalendar.getInstance(); // creates a new calendar instance
		Collections.sort(workerTasks, Task.Comparators.STARTTIME);

		int beforeTaskDay = 0;
		int startTimeInMinutes = 0;
		int totalWorkingTime = 0;
		int totalWorkingTimeAtStart = 0;
		int previousDayTime = 0;
		int taskEndTime = 0;
		int finishTime = 0;
		boolean hadBreakOne = false;
		boolean hadBreakTwo = false;

		for (Task task : workerTasks){
			calendar = GregorianCalendar.getInstance();
			calendar.setTime(task.getDateFrom());   // assigns calendar to given date
			int minus = 0;
			int totalWorkingMinutesInDay = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - startTimeInMinutes;

			if(hadBreakTwo)
			{
				totalWorkingMinutesInDay -= 120;
			}
			else if(hadBreakOne)
			{
				totalWorkingMinutesInDay -= 60;
			}

			if (calendar.get(calendar.DAY_OF_MONTH) == beforeTaskDay){
				if(totalWorkingMinutesInDay >= 60 * 4 && hadBreakOne == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) >= finishTime + 60)
				{
					minus = 60;
					hadBreakOne = true;
				}
				if(totalWorkingMinutesInDay >= 60 * 8  && hadBreakTwo == false && calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) >= finishTime + 60)
				{
					minus = 60;
					hadBreakTwo = true;
				}
				previousDayTime += calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - taskEndTime;
				totalWorkingTime += calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration() - taskEndTime - minus;
				taskEndTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();
				finishTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();
				//totalWorkingTime += task.getDuration();

			}else {
				if (previousDayTime < 3 * 60 && previousDayTime > 0) // AT LEAST 3 HOURS PER DAY
				{
					totalWorkingTime = totalWorkingTimeAtStart + 3 * 60;
				}

				hadBreakOne = false;
				hadBreakTwo = false;

				finishTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();


				beforeTaskDay = calendar.get(calendar.DAY_OF_MONTH);
				totalWorkingTimeAtStart = totalWorkingTime;
				totalWorkingTime += task.getDuration();
				previousDayTime = task.getDuration();
				startTimeInMinutes = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE);

				taskEndTime = calendar.get(calendar.HOUR_OF_DAY) * 60 + calendar.get(calendar.MINUTE) + task.getDuration();
			}
		}
		return totalWorkingTime/60;
	}




	/**
	 * Comparator'ius for sort shedules by value of Target function
	 */
	public static class Comparators {

		public static Comparator<Schedule> TARGET = new Comparator<Schedule>() {
			public int compare(Schedule o1, Schedule o2) {
				return o1.getTargetValue() - o2.getTargetValue();
			}
		};
	}



	public List<Task> getSkipedTaskList() {
		return skipedTaskList;
	}


	public void setSkipedTaskList(List<Task> skipedTaskList) {
		this.skipedTaskList = skipedTaskList;
	}


	@Override
	public String toString() {
		return "Shedule [tasks=" + tasks + ", workers=" + workers + "]";
	}


	public List<Task> getTasks() {
		return tasks;
	}


	public void setTasks(List<Task> tasks) {
		this.tasks = tasks;
	}


	public List<Worker> getWorkers() {
		return workers;
	}


	public void setWorkers(List<Worker> workers) {
		this.workers = workers;
	}


	public Map<Worker, ArrayList<Task>> getWorkerTasks() {
		return workerTasks;
	}


	public void setWorkerTasks(Map<Worker, ArrayList<Task>> workerTasks) {
		this.workerTasks = workerTasks;
	}

	public Map<Task, ArrayList<Worker>> getTaskWorkers() {
		return taskWorkers;
	}


	public void setTaskWorkers(Map<Task, ArrayList<Worker>> taskWorkers) {
		this.taskWorkers = taskWorkers;
	}


}
