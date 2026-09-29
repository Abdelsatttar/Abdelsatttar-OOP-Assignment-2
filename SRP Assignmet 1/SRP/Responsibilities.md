# SRP Refactoring - Responsibilities

## 1. WardBoard

Responsibilities found:
- Manage the relationship between beds and patients.
- Calculate the acuity score from vital signs.
- Decide when a pager alert is needed and keep the alert log.
- Build a nurse handoff note.
- Export the ward census as CSV.

Why this is a problem: patient/bed tracking, clinical scoring, alerting, presentation, and export can all change for different reasons, so changing one concern can affect unrelated code.

## 2. CheckoutBasket

Responsibilities found:
- Store basket lines and calculate the subtotal.
- Parse coupon text and calculate discounts.
- Apply gift-wrap pricing.
- Build a customer gift message.
- Create a payment authorization code.

Why this is a problem: pricing rules, coupon syntax, customer-facing text, and payment authorization are separate concerns and can change independently.

## 3. SupportTicket

Responsibilities found:
- Store ticket information and customer messages.
- Detect ticket priority from subject/body text.
- Calculate the SLA deadline and whether it is breached.
- Build the public customer reply.
- Build an internal escalation message.

Why this is a problem: ticket data, priority rules, SLA policy, and message wording belong to different responsibilities and have different reasons to change.

## 4. LoanDesk

Responsibilities found:
- Hold loan application data.
- Calculate the risk score.
- Decide loan eligibility.
- Build the required-document list.
- Build the customer decision letter.
- Export an underwriting CSV row.

Why this is a problem: financial rules, compliance documents, customer wording, and export format should not all live in the same class.

## 5. CourseEnrollmentDesk

Responsibilities found:
- Keep enrolled students and the waitlist.
- Register students and promote people from the waitlist.
- Create the welcome packet markdown.
- Create the tuition invoice line.

Why this is a problem: enrollment state is a business process, while welcome content and invoice formatting are presentation/finance concerns.

## 6. KitchenTicket

Responsibilities found:
- Store kitchen order items and ingredients.
- Detect allergens.
- Estimate preparation time.
- Render the thermal printer ticket.
- Choose the expo lane hint.

Why this is a problem: allergen rules, kitchen timing, printer formatting, and expo decisions can change independently.

## 7. SubscriptionBilling

Responsibilities found:
- Hold subscription billing data and failed-payment count.
- Calculate proration.
- Generate invoice numbers.
- Build dunning emails.
- Build accounting journal lines.

Why this is a problem: billing math, invoice numbering, collections wording, and accounting export each have a different reason to change.

## 8. WarehousePickList

Responsibilities found:
- Store picking requirements.
- Allocate available stock.
- Calculate walking order.
- Build picker instructions.
- Build the WMS XML payload.

Why this is a problem: stock allocation, warehouse routing, UI wording, and integration serialization are separate concerns.

## 9. GradeBook

Responsibilities found:
- Store student scores.
- Calculate averages.
- Apply letter-grade rules.
- Apply the honor-roll rule.
- Build a transcript document.
- Export CSV data.

Why this is a problem: score storage, academic policy, document formatting, and export formatting are different responsibilities.

## 10. AppointmentDesk

Responsibilities found:
- Track booked appointment slots.
- Apply business-hour rules.
- Search for the next available slot.
- Generate an ICS calendar event.
- Build an SMS reminder.

Why this is a problem: scheduling rules, booking state, calendar serialization, and messaging are different reasons to change.
