from blueqat import Circuit

#Declare a quantum circuit with one qubit, apply a NOT gate, then measure all qubits
TestCircuit = Circuit(1).x[0].m[:]

#Run the circuit and save the results
results = TestCircuit.run()

#print the results of the circuit
print(results)