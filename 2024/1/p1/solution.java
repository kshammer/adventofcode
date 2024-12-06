import java.io.IOException;
import java.util.stream.IntStream;

void main() {
  Path filePath = Paths.get("input.txt");
  List<Integer> left = new ArrayList<Integer>();
  List<Integer> right = new ArrayList<Integer>();
  try {
    List<String> lines = Files.readAllLines(filePath);
    lines.stream().forEach(line -> {
      // Removes excess whitespace
      String clean = line.replaceAll("\\s+", "+");
      List<String> nums = List.of(clean.split("\\+"));
      left.add(Integer.valueOf(nums.get(0)));
      right.add(Integer.valueOf(nums.get(1)));
    });
  } catch (IOException e) {
    System.out.println("FILE IS IN THE WRONG PLACE =)");
    e.printStackTrace();
  }
  Collections.sort(left);
  Collections.sort(right);
  OptionalInt output = IntStream.range(0, left.size() + 1).reduce((total, index) -> {
    int diff = Math.abs(left.get(index - 1) - right.get(index - 1));
    return total + diff;
  });
  System.out.println(output);
}
