import java.io.IOException;
import java.util.ArrayList;

void main() {
  Path filePath = Paths.get("input.txt");
  List<Integer> left = new ArrayList<Integer>();
  List<Integer> right = new ArrayList<Integer>();
  try {
    List<String> lines = Files.readAllLines(filePath);
    lines.stream().forEach(line -> {
      String clean = line.replaceAll("\\s+", "+");
      List<String> nums = List.of(clean.split("\\+"));
      left.add(Integer.valueOf(nums.get(0)));
      right.add(Integer.valueOf(nums.get(1)));
    });
  } catch (IOException e) {
    System.out.println("FILE IS IN WRONG PLACE =)");
    e.printStackTrace();
  }
  Map<Integer, Integer> values = new HashMap<Integer, Integer>();
  int total = left.stream().reduce(0, (subtotal, element) -> {

    values.computeIfAbsent(element, (val) -> {
      return Collections.frequency(right, val);
    });
    return subtotal += element * values.get(element);
  });
  System.out.println(total);

}
