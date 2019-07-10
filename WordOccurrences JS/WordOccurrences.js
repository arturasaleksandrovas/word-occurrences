var sentence = "I have a transportation device which is a red bike which I love to ride.";
var wordsOnlySentence = sentence.replace(/[^a-z0-9 ]/gi, "");

if (wordsOnlySentence.trim() == "") {
	console.log("Sorry, sentence without words was entered.");
}

// ---------- Word occurences BEGIN ----------
var wordsOccurrences = _getWordsOccurrencesFromSentence(wordsOnlySentence);

console.log("List of word occurencies:");
outputMap(wordsOccurrences);

// ---------- Word occurences END ----------

// ---------- Word lengths occurences BEGIN ----------
var wordLengthsOccurrences = _getWordLengthsOccurrencesFromSentence(wordsOnlySentence);

console.log("List of sorted word lengths occurencies:");

// On javascript object keys are sorted automatically
outputMap(wordLengthsOccurrences);

// ---------- Word lengths occurences END ----------


function outputMap(map) {
	var output = "";

	Object.keys(map).forEach(function(key) {
	  output = output + "\"" + key + ", " + map[key] + "\"" + ", ";
	});

	// Remove last ", "
	output = output.substr(0, output.length - 2);

	console.log(output);
}

function _getWordsOccurrencesFromSentence(sentence) {
	var words = sentence.split(" ");
	var occurrencesMap = {};

	for (var i = 0; i < words.length; i++) {
		if (words[i].trim() == "") 
			continue;

		var caseInsensitiveKey = Object.keys(occurrencesMap).find(function(key) {
			return words[i].toLowerCase() == key.toLowerCase();
		});

		if (occurrencesMap[caseInsensitiveKey] == undefined)
			occurrencesMap[words[i]] = 1;
		else 
			occurrencesMap[caseInsensitiveKey]++;
	}

	return occurrencesMap;
}

function _getWordLengthsOccurrencesFromSentence(sentence) {
	var words = sentence.split(" ");
	var occurrencesMap = {};

	for (var i = 0; i < words.length; i++) {
		if (words[i].trim() == "") 
			continue;

		if (occurrencesMap[words[i].length] == undefined)
			occurrencesMap[words[i].length] = 1;
		else
			occurrencesMap[words[i].length]++;
	}

	return occurrencesMap;
}