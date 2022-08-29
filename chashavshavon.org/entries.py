import json


class EntryLists:
    def __init__(self, json_text):
        self.entryList = []
        self.kavuahList = []
        self.taharaEventsList = []

        lists = json.loads(json_text.encode("utf-8"))

        for entry in lists['Entries']:
            self.entryList.append(Entry(entry))

        for kavuah in lists['Kavuahs']:
            self.kavuahList.append(Kavuah(kavuah))

        for taharaEvent in lists['TaharaEvents']:
            self.taharaEventsList.append(TaharaEvent(taharaEvent))


class Entry:
    def __init__(self, entry):
        self.isInvisible = entry['IsInvisible']
        self.date = entry['When']
        self.dn = 'Day' if entry['DN'] == '1' else 'Night'
        self.notes = entry['Notes']


class Kavuah:
    def __init__(self, kavuah):
        self.active = kavuah['Active']
        self.type = kavuah['KavuahDescriptionHebrew']
        self.dn = 'Day' if kavuah['DayNight'] == '1' else 'Night'
        self.number = kavuah['Number']
        self.cancels = kavuah['CancelsOnahBeinanis']
        self.notes = kavuah['Notes']


class TaharaEvent:
    def __init__(self, tahara_event):
        self.type = tahara_event['TaharaEventTypeName']
        self.date = tahara_event['DateTime']
        self.jewish_date = tahara_event['JewishDate']
        self.notes = tahara_event['Notes']
