using SrpLab;

Console.WriteLine("SrpLab — SRP refactored version");
Console.WriteLine("================================");

var acuityScorer = new AcuityScorer();
var wardCensus = new WardCensus();
var pager = new PagerAlertService();
var handoffFormatter = new HandoffNoteFormatter();
var censusExporter = new CensusCsvExporter();

var acuity = acuityScorer.Score(130, 89);
wardCensus.AssignBed(1, "p-88", acuity);
pager.CheckAndRecord(1, acuity);
Console.WriteLine(handoffFormatter.Format(1, "P-88", acuity));
Console.WriteLine(string.Join(" | ", pager.DrainLog()));
Console.WriteLine(censusExporter.Export(wardCensus.Patients, wardCensus.AcuityScores));

var basket = new CheckoutBasket();
basket.AddLine("SKU-1", 40m, 2);
basket.ApplyCouponText("SAVE10");
basket.EnableGiftWrap();
var discountCalculator = new CouponDiscountCalculator();
var totalCalculator = new CheckoutTotalCalculator();
var basketTotal = totalCalculator.Calculate(basket, discountCalculator);
var giftFormatter = new GiftMessageFormatter();
var paymentAuthorizer = new PaymentAuthorizerStub();
Console.WriteLine($"basket total={basketTotal} auth={paymentAuthorizer.Authorize(basket, "4242", basketTotal)}");
Console.WriteLine(giftFormatter.Format(basket, "Ahmed", basketTotal));

var ticket = new SupportTicket("T-1", "cannot login", "prod is down for me", DateTimeOffset.UtcNow);
var priorityCalculator = new TicketPriorityCalculator();
var slaPolicy = new SlaPolicy();
var priority = priorityCalculator.Calculate(ticket);
var deadline = slaPolicy.GetDeadline(ticket, priority);
var replyFormatter = new SupportReplyFormatter();
Console.WriteLine(replyFormatter.Format(ticket, priority, deadline, "Nora"));

var loan = new LoanApplication(60_000m, 640, 4, hasCollateral: false);
var riskCalculator = new LoanRiskCalculator();
var eligibilityPolicy = new LoanEligibilityPolicy();
var documentPolicy = new LoanDocumentPolicy();
var risk = riskCalculator.Calculate(loan);
var eligible = eligibilityPolicy.IsEligible(loan, risk);
var documents = documentPolicy.RequiredDocuments(loan, eligible);
var decisionFormatter = new LoanDecisionLetterFormatter();
Console.WriteLine(decisionFormatter.Format(loan, "Omar", risk, eligible, documents));

var course = new CourseEnrollment("SEF-101", capacity: 1, tuition: 3000m);
Console.WriteLine(course.Register("a@mail.com"));
Console.WriteLine(course.Register("b@mail.com"));
var welcomeFormatter = new CourseWelcomePacketFormatter();
Console.WriteLine(welcomeFormatter.Format(course, "b@mail.com", "Bea"));

var kitchenOrder = new KitchenOrder();
kitchenOrder.AddItem("Pasta", new[] { "wheat", "milk" }, 12);
var allergenDetector = new AllergenDetector();
var allergens = allergenDetector.Detect(kitchenOrder);
var readyTimeCalculator = new KitchenReadyTimeCalculator();
var readyMinutes = readyTimeCalculator.Calculate(kitchenOrder, 2, allergens.Count);
var thermalFormatter = new ThermalTicketFormatter();
Console.WriteLine(thermalFormatter.Format(kitchenOrder, 42, readyMinutes, allergens));

var subscription = new Subscription("c-9", 99m, new DateOnly(2026, 9, 1), new DateOnly(2026, 10, 1));
subscription.RegisterFailedPayment();
var prorationCalculator = new ProrationCalculator();
var invoiceGenerator = new InvoiceNumberGenerator();
var invoiceNumber = invoiceGenerator.Next(subscription.PeriodStart);
var dunningFormatter = new DunningEmailFormatter();
Console.WriteLine(dunningFormatter.Format(subscription, "Sara", new DateOnly(2026, 9, 20),
    prorationCalculator.Calculate(subscription, subscription.PeriodStart), invoiceNumber));

var pickList = new WarehousePickList();
pickList.AddNeed("BOLT", "A", 3, 10, 7);
pickList.AddNeed("NUT", "B", 1, 5, 5);
var allocator = new StockAllocator();
var allocations = allocator.Allocate(pickList);
var walkingOrderCalculator = new WalkingOrderCalculator();
var walkingOrder = walkingOrderCalculator.Calculate(pickList);
var pickerScriptFormatter = new PickerScriptFormatter();
Console.WriteLine(pickerScriptFormatter.Format(pickList, allocations, walkingOrder));

var grades = new GradeBook();
grades.Record("s1", 92);
grades.Record("s1", 88);
var gradePolicy = new LetterGradePolicy();
var honorPolicy = new HonorRollPolicy();
var average = grades.Average("s1");
var letter = gradePolicy.GetLetter(average);
var honor = honorPolicy.Meets(average, letter);
var transcriptFormatter = new TranscriptFormatter();
Console.WriteLine(transcriptFormatter.Format("s1", "Ali", average, letter, honor));

var hours = new BusinessHoursPolicy(new TimeOnly(9, 0), new TimeOnly(17, 0), 30);
var scheduler = new AppointmentScheduler(hours);
var slot = scheduler.FindNextSlot(DateTimeOffset.Parse("2026-09-21T08:00:00Z"), 48);
if (slot is null) throw new InvalidOperationException("no slot");
scheduler.TryBook(slot.Value);
var smsFormatter = new SmsReminderFormatter();
Console.WriteLine(smsFormatter.Format(slot.Value, "0100"));

Console.WriteLine("Done. Responsibilities are separated into cohesive classes.");
