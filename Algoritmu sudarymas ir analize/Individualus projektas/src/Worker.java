/**
 * 
 *  worker class
 *
 */
public class Worker  implements Comparable<Worker>{ 

	/** identifier*/
	private String id;
	
	/** maximum working time per month */
	private int maxHoursPerMonth;

	private int airLineCnt;

	private String Airline1;
	private String Airline2;
	private String Airline3;


	Worker(String id, int maxHoursPerMonth){
    	// assigning by constructor passed values 
    	this.id = id;
    	this.maxHoursPerMonth = maxHoursPerMonth;
		this.airLineCnt = 0;
		this.Airline1 = "";
		this.Airline2 = "";
		this.Airline3 = "";
    }
	
	public int compareTo(Worker other){
        return this.id.compareTo(other.id);
    }
	
	// getters and setters (auto-generated)
	
	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public int getMaxHoursPerMonth() {
		return maxHoursPerMonth;
	}

	public void setMaxHoursPerMonth(int maxHoursPerMonth) {
		this.maxHoursPerMonth = maxHoursPerMonth;
	}

	public void setAirline1(String name)
	{
		this.Airline1 = name;
	}
	public void setAirline2(String name)
	{
		this.Airline2 = name;
	}
	public void setAirline3(String name)
	{
		this.Airline3 = name;
	}

	public String getAirline1()
	{
		return Airline1;
	}
	public String getAirline2()
	{
		return Airline2;
	}
	public String getAirline3()
	{
		return Airline3;
	}

	public void setAirLineCnt(int cnt)
	{
		this.airLineCnt = cnt;
	}
	public int getAirLineCnt()
	{
		return airLineCnt;
	}
}