import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Comparator;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.concurrent.TimeUnit;

/**
 * Task class
 *
 */
public class Task  implements Comparable<Task>{ 

		
	private String id;

	private String idAirline;
	
	/** flight number */
	private String dateFromStr;

	/** task duration*/
	private int duration;

	private int workers;

	private int workersComplete;
	
	
	// additional information creating together with the object, while 

	
	private int taskStartTime;
	
	private int taskEndTime;
	
	// getters and setters ------------------
	
	private int taskStartHourInDay;
	
	private Date dateFrom;
	
	
	
	/** Constructor creating the task Object */
	Task(String id, String idAirline, String dateFlightFromStr, int duration, int workers) throws ParseException{
		// assign mane data to object
		this.id = id;

		this.idAirline = idAirline;
		
		this.dateFromStr = dateFlightFromStr;
		
		this.duration = duration;

		this.workers = workers;

		this.workersComplete = 0;
	
		
		// format additional task object, witch later will be used for schedule construction
		
		DateFormat formatter = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Date dateFrom = formatter.parse(dateFromStr);
		Calendar calendar = GregorianCalendar.getInstance(); // creates a new calendar instance
		calendar.setTime(dateFrom);   // assigns calendar to given date 
		calendar.add(Calendar.MINUTE, -1*this.duration); 
		this.taskStartHourInDay = calendar.get(Calendar.HOUR_OF_DAY);
		this.dateFrom = calendar.getTime();
		
		long diff = dateFrom.getTime() - Config.getDateFrom().getTime(); 
		this.taskStartTime =  (int) (TimeUnit.MILLISECONDS.toMinutes(diff)) - this.duration;
		this.taskEndTime =  (int) (TimeUnit.MILLISECONDS.toMinutes(diff));

	}
	
	
	
	public int compareTo(Task other){
		 return this.id.compareTo(other.id);
	}
	
	public static class Comparators {
        public static Comparator<Task> STARTTIME = new Comparator<Task>() {
            public int compare(Task o1, Task o2) {
                return o1.getTaskStartTime()  - o2.getTaskStartTime();
            }
        };
    }

	@Override
	public String toString() {
		return "Task [id=" + id + ", idAirline=" + idAirline + ", workers=" + workers +", dateFromStr=" + dateFromStr + ", duration=" + duration + ", taskStartTime="
				+ taskStartTime + ", taskEndTime=" + taskEndTime + ", workingPeriodFrom=" 
				+ ", taskStartHourInDay=" + taskStartHourInDay + ", dateFrom=" + dateFrom + "]";
	}
	
	/** method used to sort tasks by ID*/

	public String getId() {
		return id;
	}
	
	public void setId(String id) {
		this.id = id;
	}

	public String getIdAirline() {
		return idAirline;
	}

	public void setIdAirline(String id) {
		this.idAirline = id;
	}
	
	public String getDateFromStr() {
		return dateFromStr;
	}

	public void setDateFromStr(String dateFromStr) {
		this.dateFromStr = dateFromStr;
	}

	public int getDuration() {
		return duration;
	}

	public void setDuration(int duration) {
		this.duration = duration;
	}

	public int getWorkers() {
		return workers;
	}

	public void setWorkers(int workers) {
		this.workers = workers;
	}

	public int getWorkersComplete() {
		return workersComplete;
	}

	public void addWorkersComplete() {
		this.workersComplete++;
	}

	public void setWorkersComplete(int workersComplete) {
		this.workersComplete = workersComplete;
	}

	public int getTaskStartTime() {
		return taskStartTime;
	}

	public void setTaskStartTime(int taskStartTime) {
		this.taskStartTime = taskStartTime;
	}
	
	public int getTaskEndTime() {
		return taskEndTime;
	}

	public void setTaskEndTime(int taskEndTime) {
		this.taskEndTime = taskEndTime;
	}

	public int getTaskStartHourInDay() {
		return taskStartHourInDay;
	}

	public void setTaskStartHourInDay(int taskStartHourInDay) {
		this.taskStartHourInDay = taskStartHourInDay;
	}

	public Date getDateFrom() {
		return dateFrom;
	}

	public void setDateFrom(Date dateFrom) {
		this.dateFrom = dateFrom;
	}

}