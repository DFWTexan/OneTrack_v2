export class ConstantsDataService {
  private readonly appointmentstatuses = [
    'Active',
    'Pending',
    'In-Active',
    'History',
  ];
  private readonly agentStatuses = [
    'Active',
    'Pending',
    'In-Active',
    'Leave',
    'Terminated',
  ];
  private readonly applicationStatuses = [
    'Accepted',
    'Discontinued',
    'History',
    'Pending',
    'Lost',
    'Rejected',
    'Resubmitted',
  ];
  private readonly preEducationStatuses = [
    'Extend 1',
    'Extend 2',
    'Extend 3',
    'Extend 3+',
    'Pending',
    'Taken',
    'No Show',
    'Rescheduled',
    'Cancelled',
  ];
  private readonly preExamStatuses = [
    'Pending',
    'Passed',
    'Failed',
    'No Show',
    'Rescheduled',
  ];
  private readonly states = [
    'AL',
    'AK',
    'AZ',
    'AR',
    'CA',
    'CO',
    'CT',
    'DE',
    'FL',
    'GA',
    'HI',
    'ID',
    'IL',
    'IN',
    'IA',
    'KS',
    'KY',
    'LA',
    'ME',
    'MD',
    'MA',
    'MI',
    'MN',
    'MS',
    'MO',
    'MT',
    'NE',
    'NV',
    'NH',
    'NJ',
    'NM',
    'NY',
    'NC',
    'ND',
    'OH',
    'OK',
    'OR',
    'PA',
    'RI',
    'SC',
    'SD',
    'TN',
    'TX',
    'UT',
    'VT',
    'VA',
    'WA',
    'WV',
    'WI',
    'WY',
  ];
  private readonly stateProvince = [
    'AL',
    'AK',
    'AZ',
    'AR',
    'CA',
    'CO',
    'CT',
    'DE',
    'FL',
    'GA',
    'HI',
    'ID',
    'IL',
    'IN',
    'IA',
    'KS',
    'KY',
    'LA',
    'ME',
    'MD',
    'MA',
    'MI',
    'MN',
    'MS',
    'MO',
    'MT',
    'NE',
    'NV',
    'NH',
    'NJ',
    'NM',
    'NY',
    'NC',
    'ND',
    'OH',
    'OK',
    'OR',
    'PA',
    'RI',
    'SC',
    'SD',
    'TN',
    'TX',
    'UT',
    'VT',
    'VA',
    'WA',
    'WV',
    'WI',
    'WY',
    'AB',
    'BC',
    'MB',
    'NB',
    'NL',
    'NS',
    'NT',
    'NU',
    'ON',
    'PE',
    'QC',
    'SK',
    'YT',
  ];

  getPreExamStatuses() {
    return this.preExamStatuses;
  }

  getPreEducationStatuses() {
    return this.preEducationStatuses;
  }

  getApplicationStatuses() {
    return this.applicationStatuses;
  }

  getAppointmentStatuses() {
    return this.appointmentstatuses;
  }

  getAgentStatuses() {
    return this.agentStatuses;
  }

  getStates() {
    return this.states.sort();
  }

  getStateProvinces() {
    return this.stateProvince.sort();
  }
}
