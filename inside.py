# -*- coding: utf-8 -*-
"""
Created on Sat May 29 19:25:24 2021

@author: Yacine
"""
from os import path, mkdir, system

file = path.expandvars(r'%APPDATA%\INSIDE\data.dat')
if not path.exists(path.expandvars(r'%APPDATA%\INSIDE')):
    mkdir(path.expandvars(r'%APPDATA%\INSIDE'))

# Extraction of data from SetPhase and Play instructions 
# Saving data in a csv file

def Visualization (schedule, backend, dt):
    config = backend.configuration()
    if config.n_qubits>0 and config.n_qubits<6 :
        PulseDataExtraction(schedule, dt,config.n_qubits)
        system('INSIDE.exe')
    else:
        print("Error: inside is currently limited to 5 Qubits", sys.exc_info()[0])

def PulseDataExtraction (schedule, dt2, nqubit):
    
    Header1 = [u'',u'Qubit0',u'Qubit0',u'Qubit0',u'Qubit0',u'Qubit0',
                    u'Qubit0',u'Qubit0',u'Qubit0',u'Qubit0',
                u'Qubit1',u'Qubit1',u'Qubit1',u'Qubit1',u'Qubit1',
                    u'Qubit1',u'Qubit1',u'Qubit1',u'Qubit1',
                u'Qubit2',u'Qubit2',u'Qubit2',u'Qubit2',u'Qubit2',
                    u'Qubit2',u'Qubit2',u'Qubit2',u'Qubit2',
                u'Qubit3',u'Qubit3',u'Qubit3',u'Qubit3',u'Qubit3',
                    u'Qubit3',u'Qubit3',u'Qubit3',u'Qubit3',
                u'Qubit4',u'Qubit4',u'Qubit4',u'Qubit4',u'Qubit4',
                    u'Qubit4',u'Qubit4',u'Qubit4',u'Qubit4',
                u'Qubit5',u'Qubit5',u'Qubit5',u'Qubit5',u'Qubit5',
                    u'Qubit5',u'Qubit5',u'Qubit5',u'Qubit5',
                u'Qubit6',u'Qubit6',u'Qubit6',u'Qubit6',u'Qubit6',
                    u'Qubit6',u'Qubit6',u'Qubit6',u'Qubit6',
                u'Qubit7',u'Qubit7',u'Qubit7',u'Qubit7',u'Qubit7',
                    u'Qubit7',u'Qubit7',u'Qubit7',u'Qubit7',
                u'Qubit8',u'Qubit8',u'Qubit8',u'Qubit8',u'Qubit8',
                    u'Qubit8',u'Qubit8',u'Qubit8',u'Qubit8',
                u'Qubit9',u'Qubit9',u'Qubit9',u'Qubit9',u'Qubit9',
                    u'Qubit9',u'Qubit9',u'Qubit9',u'Qubit9',
                u'Qubit10',u'Qubit10',u'Qubit10',u'Qubit10',u'Qubit10',
                    u'Qubit10',u'Qubit10',u'Qubit10',u'Qubit10',
                u'Qubit11',u'Qubit11',u'Qubit11',u'Qubit11',u'Qubit11',
                    u'Qubit11',u'Qubit11',u'Qubit11',u'Qubit11',
                u'Qubit12',u'Qubit12',u'Qubit12',u'Qubit12',u'Qubit12',
                    u'Qubit12',u'Qubit12',u'Qubit12',u'Qubit12',
                u'Qubit13',u'Qubit13',u'Qubit13',u'Qubit13',u'Qubit13',
                    u'Qubit13',u'Qubit13',u'Qubit13',u'Qubit13',
                u'Qubit14',u'Qubit14',u'Qubit14',u'Qubit14',u'Qubit14',
                    u'Qubit14',u'Qubit14',u'Qubit14',u'Qubit14',
                u'Qubit15',u'Qubit15',u'Qubit15',u'Qubit15',u'Qubit15',
                    u'Qubit15',u'Qubit15',u'Qubit15',u'Qubit15',
                ]
    Header2 = [u'temps',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                u'ShiftPhaseDrive',u'DriveChannel',u'DriveChannel',
                u'ShiftPhaseControl',u'ControlChannel',u'ControlChannel',
                u'ShiftPhaseAcquire','AcquireChannel','AcquireChannel',
                ]
    
    Header3 = [u'',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',u'',u'Re()',u'Im()',
                ]
    
    NumberQubit = 16
    data = [''] * 145
    data[0] ='0'
    qubit=0
    name=''
    phase=0
    ListPhase = [0]*(3*NumberQubit)
    Index_instr = 0
    len_type=0
    
    f = open(file, 'w+')
    f.write(str(nqubit)+"\n")
    f.write(str(dt)+"\n")
    # Writing headers
    Header_line1 = ";".join(Header1) + "\n"
    f.write(Header_line1)
    Header_line2 = ";".join(Header2) + "\n"
    f.write(Header_line2)
    Header_line3 = ";".join(Header3) + '\n'
    f.write(Header_line3)
    
    #Loop on instructions
    for i in schedule.instructions:
        Index_instr= Index_instr+1
        if (data[0]!=''.join(str(i[0]))):
            data[0] =str(i[0])
            
        if data[0] == ''.join(str(i[0])):
            len_type =len(str(type(i[1])))
            if len_type ==52: #SetPhaseInstruction
                qubit = i[1].channel.index
                name = i[1].channel.name[0]
                phase = i[1].phase
                
                if  name =='d':#Drive
                    ListPhase[(qubit*3)] = ListPhase[(qubit*3)]+phase
                    data[(qubit*9)+1]=str(ListPhase[(qubit*3)])
                    
                if name =='u':#Control
                    ListPhase[(qubit*3)+1] = ListPhase[(qubit*3)+1]+phase
                    data[(qubit*9)+4]=str(ListPhase[(qubit*3)+1])
                    
                if name =='a':#Acquire
                    ListPhase[(qubit*3)+2] = ListPhase[(qubit*3)+2]+phase
                    data[(qubit*9)+7]=str(ListPhase[(qubit*3)+2])
                    
                    
            if len_type ==45:#PlayInstruction
                qubit = i[1].channel.index
                name = i[1].channel.name[0]
                Re= ['']*int(len(i[1].pulse.samples))
                Im= ['']*int(len(i[1].pulse.samples))
                ind = 0
                for k in i[1].pulse.samples:#Saving points
                     Re[ind] = str(k.real)
                     Im[ind] = str(k.imag)
                     ind= ind+1
                     
                for dt in range(len(i[1].pulse.samples)):
                    tmps= int(data[0])+ dt*dt2
                    data[0] = str(tmps)
                    
                    if name =='d':#Drive
                         data[qubit+2]=str(Re[dt])
                         data[qubit+3]=str(Im[dt])
                         
                    if name =='u':#Control
                         data[qubit+5]=str(Re[dt])
                         data[qubit+6]=str(Im[dt])
                         
                    if name =='a':#Acquire
                        data[qubit+8]=str(Re[dt])
                        data[qubit+9]=str(Im[dt])
                    
                    #Writing data
                    data[0] = str(data[0])
                    data_csv = ";".join(data) + "\n"
                    f.write(data_csv)
                    data[0] =str(i[0])
                    
    
    f.close()

