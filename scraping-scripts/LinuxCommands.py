from bs4 import BeautifulSoup
import requests as re 
import sqlite3 
from sqlite3 import Error

def create_connection(db_file):
    """ create a database connection to the SQLite database
        specified by db_file
    :param db_file: database file
    :return: Connection object or None
    """
    conn = None
    try:
        conn = sqlite3.connect(db_file)
    except Error as e:
        print(e)

    return conn

def create_command(conn, command):
    """
    Create a new command into the commands table
    :param conn:
    :param command:
    :return: command id
    """
    sql = ''' INSERT INTO commands(name,platform,description,link)
              VALUES(?,?,?,?) '''
    cur = conn.cursor()
    cur.execute(sql, command)
    conn.commit()
    return cur.lastrowid

def scrapeCommands(url):
    
    site_content = re.get(url).text

    soup = BeautifulSoup(site_content, "html.parser")

    table = soup.find("table",{"class":"az"})

    rows = table.findAll('tr')

    for tr in rows:
        cols = tr.findAll('td')

        # get the command name
        name = cols[1].text.strip()
        # get the command description
        desc = cols[2].text.strip()
        # the command platform is always Linux
        platform = "Linux"
        # get the command link if any
        link = ""
        for l in cols[1].find_all('a'):
            link = ""
            link = l.get('href').strip()
            if link:
                link = url + str(link)
                break

        if name == "" or desc == "" or link == "":
            yield None
        yield (str(name), str(desc), str(platform), str(link))
       

def main():
    database = "../API.db"

    url = "https://ss64.com/bash/"
    
    # create a database connection
    conn = create_connection(database)

    with conn:

        # for each command gets scraped
        for cmd in scrapeCommands(url):        
            # create commands
            if cmd != None:
                # print(cmd.name)
                create_command(conn, cmd)

# RUN ONLY ONCE TO POPULATE THE DATABASE, THEN DISABLE IT
if __name__ == '__main__':
    print("Uncomment the next line to run the script, WARNING: This will populate the database again which might lead to duplicate data.")
   # main()