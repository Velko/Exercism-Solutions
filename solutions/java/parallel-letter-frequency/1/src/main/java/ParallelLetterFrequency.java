import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;

class ParallelLetterFrequency {

    ConcurrentMap<Character, Integer> letterCount;
    List<String> texts;

    ParallelLetterFrequency(String[] texts) {
        letterCount = new ConcurrentHashMap<>();
        this.texts = List.of(texts);
    }

    Map<Character, Integer> countLetters() {
        texts.parallelStream().forEach(text -> {
            for (char c: text.toLowerCase().toCharArray()) {
                if (Character.isAlphabetic(c)) {
                    letterCount.merge(c, 1, Integer::sum);
                }
            }
        });

        return letterCount;
    }

}
