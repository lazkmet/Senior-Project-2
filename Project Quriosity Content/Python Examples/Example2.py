from blueqat import Circuit

#Declare a circuit with 2 qubits, then:
#   -Execute the identity gate on qubit 0
#   -Execute a NOT gate on both qubits
#   -Measure the circuit
TestCircuit = Circuit(2).i[0].x[0].x[1].m[:]

#Run our test circuit and save the results
results = TestCircuit.run()

#Run the test circuit a second time and display the circuit
TestCircuit.run(backend="draw")

#Output the circuit results to the console
print(results)