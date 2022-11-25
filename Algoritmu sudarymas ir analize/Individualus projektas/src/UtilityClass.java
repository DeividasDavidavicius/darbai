import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class UtilityClass {

	public static List<Task> getTasks(String tasksFile) throws IOException, ParseException{
		List<Task> tasks = new ArrayList<Task>();
		BufferedReader br = new BufferedReader(new FileReader(tasksFile));
		String line;
		int cnt = 1;
		while ((line = br.readLine()) != null) {
			String parts[] = line.split("\\t");

			String dateFromStr = parts[0].substring(1, parts[0].length() - 1);
			int duration = Integer.parseInt(parts[1]);
			int workers = Integer.parseInt(parts[2]);
			String identifier = parts[3].substring(1, parts[3].length() - 1);
			String ID = "ID" + cnt;

			Task task = new Task(ID, identifier, dateFromStr, duration, workers);

			tasks.add(task);
			cnt++;
		}
		br.close();
		return tasks;
	}
	


	public static List<Worker> getWorkers(int workerCount)  throws NumberFormatException, IOException {

		int count164 = 0;
		int newCount = workerCount - 10;

		if(newCount <= 0)
		{
			count164 = workerCount;
		}
		else
		{
			count164 = newCount / 2 + 10;
		}

		List<Worker> workers = new ArrayList<Worker>();
		for(int i = 0; i < workerCount; i++)
		{
			int number = i + 1;
			String Ind = "ID" + number;
			int maxWorkingHours = 0;
			if(count164 > i)
			{
				maxWorkingHours = 164;
			}
			else
			{
				maxWorkingHours = 84;
			}
			Worker worker = new Worker(Ind, maxWorkingHours);
			workers.add(worker);
		}
		return workers;
	}
	
	

	public static void printResults(Schedule shedule, String directory) throws FileNotFoundException, UnsupportedEncodingException{
		PrintWriter writer;
		for(Worker worker : shedule.getWorkers()){
			writer = new PrintWriter(directory + "/" + worker.getId() + ".txt", "UTF-8");
			writer.println("Total working hours: " + shedule.totalWorkingTime(shedule.getWorkerTasks().get(worker)));
			
			List<Task> workerTasks =  shedule.getWorkerTasks().get(worker); 
			Collections.sort(workerTasks, Task.Comparators.STARTTIME);
			
			for(Task task : workerTasks){
				writer.println(task);
			}
			writer.close();
		}
		
	}
	
	
}
